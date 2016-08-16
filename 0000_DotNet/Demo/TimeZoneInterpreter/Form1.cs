using System;
using System.Globalization;
using System.Windows.Forms;

using TimeZoneInterpreter.Properties;

namespace TimeZoneInterpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var rightMenu = new ToolStripMenuItem("Copy");
            rightMenu.Click += Copy_Click;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { rightMenu });
            listBox1.ContextMenuStrip = contextMenuStrip1;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = Resources.Form1_Form1_yyyy_MM_dd_HH_mm_ss;

            toolStripStatusLabel1.Text = "Your current Time zone:" + TimeZoneInfo.Local.DisplayName;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();

            var listOfTimeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var zone in listOfTimeZones)
            {
                if (!(comboBox1.SelectedItem as TimeZoneInfo).Equals(zone) && (comboBox1.SelectedItem as TimeZoneInfo).BaseUtcOffset != zone.BaseUtcOffset)
                {
                    checkedListBox1.Items.Add(zone);
                    comboBox1.Items.Add(zone);
                }
            }
            toolStripStatusLabel1.Text = "Your current Time zone:" + comboBox1.SelectedItem;
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            string copyText = "";
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                copyText = copyText + Environment.NewLine + listBox1.SelectedItems[i];
            }
            Clipboard.SetText(copyText);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;

            var listOfTimeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var zone in listOfTimeZones)
            {
                if (!TimeZoneInfo.Local.Equals(zone) && TimeZoneInfo.Local.BaseUtcOffset!=zone.BaseUtcOffset)
                {
                    checkedListBox1.Items.Add(zone);
                    comboBox1.Items.Add(zone);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            TimeZoneInfo tz = comboBox1.SelectedItem as TimeZoneInfo;
            DateTime dt;

            DateTime now = DateTime.SpecifyKind(dateTimePicker1.Value, DateTimeKind.Unspecified);

            if (tz == null)
            {
                dt = TimeZoneInfo.ConvertTimeToUtc(now, TimeZoneInfo.Local);
            }
            else
            {
                dt = TimeZoneInfo.ConvertTimeToUtc(now, tz);
            }
            
            DateTime convertDT;
            foreach (object t in checkedListBox1.CheckedItems)
            {
                convertDT = TimeZoneInfo.ConvertTimeFromUtc(dt, t as TimeZoneInfo);
                listBox1.Items.Add(t + "  " + convertDT.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
