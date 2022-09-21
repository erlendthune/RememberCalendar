using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Timers;
using Ical.Net;
using RestSharp;

namespace RememberCalendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private delegate void SafeCallDelegate(string text);
        List<Ical.Net.DataTypes.Occurrence> appointmentList = new List<Ical.Net.DataTypes.Occurrence>();
        string projectUrl = "https://github.com/erlendthune/RememberCalendar";

        private void WriteTextSafe(string text)
        {
            if (labelUpcomingAppointment.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                labelUpcomingAppointment.Invoke(d, new object[] { text });
            }
            else
            {
                labelUpcomingAppointment.Text = text;
                labelUpcomingAppointment.BackColor = label1.BackColor == Color.Red ? Color.Green : Color.Red;
            }
        }

        // This is the method to run when the timer is raised.
        private void OnTimedEvent(Object myObject,
                                                EventArgs myEventArgs)
        {
            if(AppointmentComingUpSoon())
            {
                SystemSounds.Beep.Play();
                WriteTextSafe("UPCOMING EVENT");
            }
        }

        private bool AppointmentComingUpSoon()
        {
            foreach (var appointment in appointmentList)
            {
                var alertTime = appointment.Period.StartTime.AddMinutes(-15); 
                if(DateTime.Now.Ticks > alertTime.Ticks)
                {
                    return true;
                }

            }
            return false;
        }

        public void StartTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
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
                        appointmentList.Add(occ);
                    }
                    else
                    {
                        richTextBox1.Text += $"{item.Summary} has passed: {appointmentDateTime}\n";
                    }
                }
            }
            upcomingAppointmentTextBox.Text = "";
            appointmentList.Sort((a, b) => a.Period.StartTime.Ticks.CompareTo(b.Period.StartTime.Ticks));
            foreach (var occ in appointmentList)
            {
                var appointment = new DateTime(occ.Period.StartTime.Ticks);
                upcomingAppointmentTextBox.Text += $"{appointment}\n";
            }
            StartTimer();
        }
        private async Task GetCalenderRest()
        {
            appointmentList.Clear();

            var restClient = new RestClient(textBoxBaseURL.Text);
            var request = new RestRequest(textboxIcsUrl.Text);
            
            var response = await restClient.ExecuteGetAsync(request);
            if (response.IsSuccessful)
            {
                ParseIcsCalendar(response.Content);
            }
        }
 
        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            await this.GetCalenderRest();
        }
        private async Task GetCalendarHttpClient()
        {
            HttpClient client = new HttpClient() {
                BaseAddress = new Uri(textBoxBaseURL.Text)   //Ensure the URL ends with a slash...
            };
            client.DefaultRequestHeaders.Add("User-Agent", "RememberCalendar");
            var response = await client.GetAsync(textboxIcsUrl.Text);
            if (response.IsSuccessStatusCode)
            {
                string icaltext = await response.Content.ReadAsStringAsync();
                ParseIcsCalendar(icaltext);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await this.GetCalendarHttpClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxBaseURL.Text = Properties.Settings.Default.baseURL;
            textboxIcsUrl.Text = Properties.Settings.Default.icsRelativeURL;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.icsRelativeURL = textboxIcsUrl.Text;
            Properties.Settings.Default.Save();
        }
    }
}