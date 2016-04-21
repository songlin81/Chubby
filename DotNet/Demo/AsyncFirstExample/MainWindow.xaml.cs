using System;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;

namespace AsyncFirstExample
{
    public partial class MainWindow : Window
    {
        // Part 1.
        // Mark the event handler with async so you can use await in it.
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = await AccessTheWebAsync();
            resultsTextBox.Text += String.Format("\r\nLength of the downloaded string: {0}.\r\n", contentLength);
        }

        async Task<int> AccessTheWebAsync()
        { 
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            DoIndependentWork();

            // The await operator suspends AccessTheWebAsync.
            //  - AccessTheWebAsync can't continue until getStringTask is complete.
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.
            //  - Control resumes here when getStringTask is complete. 
            //  - The await operator then retrieves the string result from getStringTask.
            string urlContents=await getStringTask;

            // The return statement specifies an integer result.
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
            return urlContents.Length;
        }

        void DoIndependentWork()
        {
            resultsTextBox.Text += "Working . . . . . . .\r\n";
        }


        // Part 2.
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var demo = new MyClass();
            string result = await demo.DoStuff();
            resultsTextBox.Text += result;
        }
    }
}

// Sample Output:
// Working . . . . . . .
// Length of the downloaded string: 41564.