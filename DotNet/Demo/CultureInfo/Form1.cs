using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceLayer;

namespace FS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //1. Current Culture.
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name);
        }

        //2. DateTime calculation.
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime firstDate = new DateTime(2000, 01, 01);
            DateTime secondDate = new DateTime(2000, 05, 31);

            TimeSpan diff = secondDate.Subtract(firstDate);
            TimeSpan diff1 = secondDate - firstDate;
            String diff2 = (secondDate - firstDate).TotalDays.ToString(System.Globalization.CultureInfo.InvariantCulture);

            MessageBox.Show(diff.ToString());
            MessageBox.Show(diff1.ToString());
            MessageBox.Show(diff2);
        }

        //3. Regex for Email validation.
        private void button3_Click(object sender, EventArgs e)
        {
            var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            label1.Text = Regex.IsMatch(textBox1.Text, pattern) ? "Valid Email address " : "Not a valid Email address ";
        }

        //4. Regex for Email validation.
        private void button4_Click(object sender, EventArgs e)
        {
            Hashtable weeks = new Hashtable
            {
                {"1", "SunDay"},
                {"2", "MonDay"},
                {"3", "TueDay"},
                {"4", "WedDay"},
                {"5", "ThuDay"},
                {"6", "FriDay"},
                {"7", "SatDay"}
            };
            MessageBox.Show(weeks["5"].ToString());
            MessageBox.Show(weeks.ContainsValue("TueDay") ? "Find" : "Not find");
            weeks.Remove("3");
            foreach (DictionaryEntry day in weeks)
            {
                MessageBox.Show(day.Key + "   -   " + day.Value);
            }
        }

        //5. Capture key event.
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter Key Pressed ");
            }
        }

        //6. NameValueCollection.
        private void button5_Click(object sender, EventArgs e)
        {
            NameValueCollection markStatus = new NameValueCollection();
            markStatus.Add("Very High", "80");
            markStatus.Add("High", "60");
            markStatus.Add("medium", "50");
            // the NameValueCollection can contain more than one values for a particular key 
            // whereas Hashtable can contain only one value for a particular key.
            markStatus.Add("Pass1", "40");
            markStatus.Add("Pass1", "44");

            foreach (string key in markStatus.Keys)
            {
                var values = markStatus.GetValues(key);
                foreach (string value in values)
                {
                    MessageBox.Show(key + " - " + value);
                }
            }
        }

        //7. TPL.
        private void button6_Click(object sender, EventArgs e)
        {
            List<SimulateRecevier> receivers = new List<SimulateRecevier>
            {
                new SimulateRecevier() {Name = "OM"},
                new SimulateRecevier() {Name = "CDB"},
                new SimulateRecevier() {Name = "IM"},
                new SimulateRecevier() {Name = "PB"},
                new SimulateRecevier() {Name = "Baldo"},
                new SimulateRecevier() {Name = "GtConf"},
                new SimulateRecevier() {Name = "Others"}
            };

            var sw = Stopwatch.StartNew();

            foreach (SimulateRecevier rec in receivers)
            {
                rec.ReceiveMessage();
            }
            Console.WriteLine("Total cost time is " + sw.Elapsed.Milliseconds);

            sw.Reset();
            sw = Stopwatch.StartNew();

            Parallel.ForEach(receivers, rec => rec.ReceiveMessage());
            Console.WriteLine("Total cost time is " + sw.Elapsed.Milliseconds);
            sw.Stop();
        }

        //8. string Equal.
        private void button7_Click(object sender, EventArgs e)
        {
            string str1 = "Equals";
            string str2 = "Equals";
            MessageBox.Show(string.Equals(str1, str2) ? "Strings are Equal() " : "Strings are not Equal() ");
        }

        //9. string Compare.
        private void button8_Click(object sender, EventArgs e)
        {
            var str1 = "csharp";
            var str2 = "CSharp";
            //  Value Condition Less than zero strA is less than strB. 
            //  Zero strA equals strB. 
            //  Greater than zero strA is greater than strB.
            var result = string.Compare(str1, str2);    //-1
            MessageBox.Show(result.ToString());
            result = string.Compare(str1, str2, true);  //0
            MessageBox.Show(result.ToString());
        }

        //10. Path IO.
        private void button9_Click(object sender, EventArgs e)
        {
            string tmpPath = "c:\\windows\\inf\\wvmic.inf";
            string fileExtension = System.IO.Path.GetExtension(tmpPath);
            string filename = System.IO.Path.GetFileName(tmpPath);
            string filenameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(tmpPath);
            string rootPath = System.IO.Path.GetPathRoot(tmpPath);
            string directory = System.IO.Path.GetDirectoryName(tmpPath);
            string fullPath = System.IO.Path.GetFullPath(tmpPath);

            MessageBox.Show(fileExtension + ' ' + filename + ' ' + filenameWithoutExtension+
                ' ' + rootPath + ' ' + directory + ' ' + fullPath);
        }

        //11. Interface Event.
        private void button10_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee("Pony", "Smith", 40, "STO") {Department = "DCL"};
            //Pony Smith from STO to DCL
        }

        //12. File IO.
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                var byteData = Encoding.ASCII.GetBytes("FileStream Test");
                var wFile = new FileStream("c:\\streamtest.txt", FileMode.Append);
                wFile.Write(byteData, 0, byteData.Length);
                wFile.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //13. EntLib service.
        private void button12_Click(object sender, EventArgs e)
        {
            AuthorizationSA authorizationSA = new AuthorizationSA();
            string result = authorizationSA.GetUser("123321");
            label1.Text = "Veridy the result: " + result;
        }

        //14. Convert Decimal.
        private void button13_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(0.2356) == 0)
            {
                Console.WriteLine("ok");
            }
            else
            {
                Console.WriteLine("Nok");    //<---
            }

            if (Convert.ToInt32(0.2356) == 0)
            {
                Console.WriteLine("ok");    //<---
            }
            else
            {
                Console.WriteLine("Nok");
            }

            if (Convert.ToDecimal(0.0000) == 0)
            {
                Console.WriteLine("ok");    //<---
            }
            else
            {
                Console.WriteLine("Nok");
            }

            if (Convert.ToInt32(0.0000) == 0)
            {
                Console.WriteLine("ok");    //<---
            }
            else
            {
                Console.WriteLine("Nok");
            }
        }


    }
}
