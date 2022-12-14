using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;
using Ical.Net;
using RememberCalendar.Properties;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace RememberCalendar
{
    public partial class FormRememberCalendar : Form
    {
        public FormRememberCalendar()
        {
            InitializeComponent();
        }
        private delegate void SafeCallDelegate(string text);
        List<AppointmentRememberCalendar> appointmentList = new List<AppointmentRememberCalendar>();
        List<CalendarAddress> icsCalendarAddressList = new List<CalendarAddress>();
        string projectUrl = "https://github.com/erlendthune/RememberCalendar";
        System.Timers.Timer aTimer = new System.Timers.Timer();
        bool dialogBoxOnDisplay = false;
        List<AlertForm> alertFormList = new List<AlertForm>();

        /// <summary>
        /// This method is neccessary to update text from the timer thread. 
        /// </summary>
        /// <param name="text"></param>
        private void WriteUpcomingAppointmentStatusTextSafe(string text)
        {
            if (labelUpcomingAppointment.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteUpcomingAppointmentStatusTextSafe);
                labelUpcomingAppointment.Invoke(d, new object[] { text });
            }
            else
            {
                if(!dialogBoxOnDisplay)
                {
                    this.TopMost = true;
                    var allScreens = System.Windows.Forms.Screen.AllScreens;
                    foreach (var screen in allScreens)
                    {
                        AlertForm alertForm = new AlertForm(this, screen);
                        alertFormList.Add(alertForm);
                        alertForm.Show();
                    }
                    dialogBoxOnDisplay = true;
                }

                labelUpcomingAppointment.Text = text;
                labelUpcomingAppointment.BackColor = label1.BackColor == Color.Red ? Color.Green : Color.Red;
            }
        }
        private void WriteUpcomingAppointmentSummaryTextSafe(string text)
        {
            if (labelUpcomingAppointmentSummary.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteUpcomingAppointmentSummaryTextSafe);
                labelUpcomingAppointmentSummary.Invoke(d, new object[] { text });
            }
            else
            {
                labelUpcomingAppointmentSummary.Text = text;
                labelUpcomingAppointmentSummary.BackColor = label1.BackColor == Color.Red ? Color.Green : Color.Red;
            }
        }
        private void WriteUpcomingAppintmentTimeSafe(string text)
        {
            if (labelUpcomingAppointmentSummary.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteUpcomingAppintmentTimeSafe);
                labelUpcomingAppointmentSummary.Invoke(d, new object[] { text });
            }
            else
            {
                labelUpcomingAppointmentSummary.Text = text;
                labelUpcomingAppointmentSummary.BackColor = label1.BackColor == Color.Red ? Color.Green : Color.Red;
            }
        }

        // This is the method to run when the timer is raised.
        private void OnTimedEvent(Object myObject,
                                                EventArgs myEventArgs)
        {
            AppointmentRememberCalendar appointment = AppointmentComingUpSoon();
            if (appointment != null)
            {
                SystemSounds.Beep.Play();
                WriteUpcomingAppointmentStatusTextSafe("UPCOMING EVENT");
                WriteUpcomingAppointmentSummaryTextSafe(appointment.Summary);

            }
        }

        private AppointmentRememberCalendar AppointmentComingUpSoon()
        {
            foreach (var appointment in appointmentList)
            {
                if(appointment.AppointmentComingUpSoon(15))
                {
                    return appointment;
                }
            }
            return null;
        }

        public void StartTimer()
        {            
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        private void ParseIcsCalendar(string ics)
        {
            Ical.Net.Calendar calendar = Ical.Net.Calendar.Load(ics);
            foreach (var item in calendar.Events)
            {
                string appointmentDateTime = item.DtStart.ToString();

                foreach (var occ in item.GetOccurrences(DateTime.Today.Date.AddDays(-1), DateTime.Today.Date.AddDays(1)))
                {
                    if (occ.Period.StartTime.Ticks > DateTime.Now.Ticks)
                    {
                        richTextBox1.Text += $"{item.Summary} is coming up: {appointmentDateTime}\n";
                        AppointmentRememberCalendar appointment = new AppointmentRememberCalendar(item.Summary, occ.Period.StartTime);
                        appointmentList.Add(appointment);
                    }
                    else
                    {
                        richTextBox1.Text += $"{item.Summary} has passed: {appointmentDateTime}\n";
                    }
                }
            }
            listUpcomingAppointments.Items.Clear();

            appointmentList.Sort((a, b) => a.When.Ticks.CompareTo(b.When.Ticks));
            foreach (var appointment in appointmentList)
            {
                var when = new DateTime(appointment.When.Ticks);
                ListViewItem item = new ListViewItem(appointment.When.ToString());
                item.SubItems.Add(appointment.Summary);
                listUpcomingAppointments.Items.Add(item);
            }
            StartTimer();
        }

        private void debugOutput(string s)
        {
            richTextBox1.Text += $"{s}\n";
        }
        private async Task GetCalenderRest()
        {
            appointmentList.Clear();

            foreach(CalendarAddress icsAddress in icsCalendarAddressList)
            {
                try
                {
                    debugOutput("Contacting " + icsAddress.FullUrl());
                    var restClient = new RestClient(icsAddress.baseUrl);
                    var request = new RestRequest(icsAddress.relativeUrl);

                    var response = await restClient.ExecuteGetAsync(request);
                    if (response.IsSuccessful)
                    {
                        ParseIcsCalendar(response.Content);
                    }
                }
                catch (Exception e)
                {
                    debugOutput(e.Message);
                }
            }
        }
 
        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            await this.GetCalenderRest();
        }
        /// <summary>
        /// Uses Microsofts system Http Client.
        /// I did not get it to work first, and tried RestSharp instead which worked.
        /// Then I figured out that User-Agent was not set by default in 
        /// HttpClient. Once that header was set it worked fine. I left
        /// both in case someone else encounters the same problem. Then
        /// they can have a look on how I made it work.
        /// </summary>
        /// <returns></returns>
        private async Task GetCalendarHttpClient()
        {
            appointmentList.Clear();
            foreach (CalendarAddress icsAddress in icsCalendarAddressList) {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(icsAddress.baseUrl)   //Ensure the URL ends with a slash...
                };
                client.DefaultRequestHeaders.Add("User-Agent", "RememberCalendar");
                var response = await client.GetAsync(icsAddress.relativeUrl);
                if (response.IsSuccessStatusCode)
                {
                    string icaltext = await response.Content.ReadAsStringAsync();
                    ParseIcsCalendar(icaltext);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await this.GetCalendarHttpClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = "appsettings.json";
                string jsonString = File.ReadAllText(fileName);
                icsCalendarAddressList = JsonSerializer.Deserialize<List<CalendarAddress>>(jsonString);
                addIcsAddressesToGUI();
            }
            catch
            {
                Debug.WriteLine("No configuration file found.");
            }

            int defaultSnoozeTimeInMinutes = Int32.Parse(ConfigurationManager.AppSettings.Get("snoozeTimeoutValueInMinutes"));
            Debug.WriteLine("Form loaded");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            // to true.
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            string url = projectUrl;
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public void AlertFormClosed(AlertForm alertForm)
        {
            alertFormList.Remove(alertForm);
            debugOutput("AlertForm closed");
            if(alertFormList.Count == 0)
            {
                dialogBoxOnDisplay = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();

            string jsonString = JsonSerializer.Serialize<List<CalendarAddress>>(icsCalendarAddressList);
            Debug.WriteLine(jsonString);
            File.WriteAllText("appsettings.json", jsonString);
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonDismiss_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Dismiss button clicked.");
            aTimer.Enabled = false;
        }

        private void buttonSnooze_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Snooze button clicked.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddIcsUrl_Click(object sender, EventArgs e)
        {
            
            var uri = new Uri(textboxIcsUrl.Text);
            string icsRelativePath = uri.PathAndQuery;
            string icsBaseUrl = uri.GetLeftPart(System.UriPartial.Authority);

            CalendarAddress calendarAddress = new CalendarAddress(icsBaseUrl, icsRelativePath);
            icsCalendarAddressList.Add(calendarAddress);
            textboxIcsUrl.Clear();
            addCalendarAddressToGui(calendarAddress);
        }
        private void addCalendarAddressToGui(CalendarAddress calendarAddress)
        {
            ListViewItem item = new ListViewItem(calendarAddress.baseUrl);
            item.SubItems.Add(calendarAddress.relativeUrl);
            listIcsAddresses.Items.Add(item);
        }
        private void addIcsAddressesToGUI()
        {
            foreach(CalendarAddress calendarAddress in icsCalendarAddressList)
            {
                addCalendarAddressToGui(calendarAddress);            
            }
        }
        private void listIcsAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listIcsAddresses.Items.Count > 0)
            {
                ListViewItem item = listIcsAddresses.SelectedItems[0];

                icsCalendarAddressList.Remove(icsCalendarAddressList[item.Index]);
                listIcsAddresses.Items.Remove(item);
            }
        }
    }
}