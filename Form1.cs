using System.ComponentModel;
using WinPretDiskuri.Data;

namespace WinPretDiskuri
{
    public partial class Form1 : Form
    {
        public class HDDEntry
        {
            public string Nume { get; set; }
            public string Marime { get; set; }
            public string PretTB { get; set; }
        }
        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Repository.GetSortedPriceTB();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.backgroundworker?redirectedfrom=MSDN&view=net-7.0
        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                resultLabel.Visible = true;
                backgroundWorker1.RunWorkerAsync();

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            MainScraper.worker = worker;
            MainScraper.Run();
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
                dataGridView1.DataSource = Repository.GetSortedPriceTB();
                resultLabel.Visible = false;
            }
        }
    }
}