using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlainTextFileSearcher.Business;

namespace PlainTextFileSearcher.Winforms
{
    public partial class Form1 : Form
    {
        private const string SEARCH = "Search";
        private const string CANCEL = "Cancel";




        public Form1()
        {
            InitializeComponent();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            

            List<String> list = new List<string>();
            

            if (btnSearch.Text == SEARCH)
            {
                btnSearch.Text = CANCEL;

                var content =  SearchResultDataService.SearchForTextinDocumentsFromSelectedFile(tbxSearch.Text);
                tbxSearchResults.AppendText(content);


                foreach (string line in list)
                {


                    tbxSearchResults.AppendText(line);
                    tbxSearchResults.AppendText(Environment.NewLine);
                }



                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                lblTimePassedMs.Text = ts.Milliseconds.ToString() + " ms";
            }
            else
            {
                btnSearch.Text = SEARCH;

                //TODO cancelation
            }
        }



        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            lblOpenedFolder.Text = folderBrowserDialog.SelectedPath;
            btnSearch.Enabled = true;
        }

        private void lblTimePassedMs_Click(object sender, EventArgs e)
        {

        }
    }
}
