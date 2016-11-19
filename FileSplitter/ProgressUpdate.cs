using System;
using System.Windows.Forms;

namespace FileSplitter
{
    public partial class ProgressUpdate : Form
    {
        public ProgressUpdate()
        {
            InitializeComponent();
        }
        public string ProgressTitle 
        {
            get 
            {
                return Text;
            }
            set 
            {
                Text = value;
            }
        }
        public string ProgressText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        public int progress
        {
            get
            {
                return this.progressBar1.Value;
            }
            set
            {
                if (value > progressBar1.Maximum && value <= Int32.MaxValue) { this.progressBar1.Maximum = value;}
                this.progressBar1.Value = value;
            }
        }
        public int maxvalue
        {
            get
            {
                return this.progressBar1.Maximum;
            }
            set
            {
                this.progressBar1.Maximum = value;
            }
        }

        private void ProgressUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
