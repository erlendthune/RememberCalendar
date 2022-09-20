using System;
using System.Collections.Generic;
using System.Globalization;
using System.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
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
        string baseAddress = "https://outlook.office365.com/";

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

        private async Task GetCalenderRest()
        {
            appointmentList.Clear();

            var restClient = new RestClient(textBoxBaseURL.Text);
            var request = new RestRequest(url.Text);
            var response = await restClient.ExecuteGetAsync(request);
            if (response.IsSuccessful)
            {
                Ical.Net.Calendar calendar = Ical.Net.Calendar.Load(response.Content); 
                foreach (var item in calendar.Events)
                {
                    string appointmentDateTime = item.DtStart.ToString();

                    foreach (var occ in item.GetOccurrences(DateTime.Today.Date.AddDays(-1), DateTime.Today.Date.AddDays(1)))
                    {
                        if (occ.Period.StartTime.Ticks > DateTime.Now.Ticks)
                        {
                            richTextBox1.Text += $"{item.Summary} er senere i dag: {appointmentDateTime}\n";
                            appointmentList.Add(occ);
                        }
                        else
                        {
                            richTextBox1.Text += $"{item.Summary} var tidligere i dag: {appointmentDateTime}\n";
                        }
                    }
                }
                upcomingAppointmentTextBox.Text = "";
                appointmentList.Sort((a, b) => a.Period.StartTime.Ticks.CompareTo(b.Period.StartTime.Ticks));
                foreach (var occ in appointmentList) {
                    var appointment = new DateTime(occ.Period.StartTime.Ticks);
                    upcomingAppointmentTextBox.Text += $"{appointment}\n";
                }
                StartTimer();
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
            var response = await client.GetAsync(url.Text);
            if (response.IsSuccessStatusCode)
            {
                string icaltext = await response.Content.ReadAsStringAsync();
//                ical.net.calendar calendar = ical.net.calendar.load(icaltext);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await this.GetCalendarHttpClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxBaseURL.Text = baseAddress;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}