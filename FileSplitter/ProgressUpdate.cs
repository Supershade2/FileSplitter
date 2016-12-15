using System;
using System.Windows.Forms;

namespace FileSplitter
{
    /*! \file ProgressUpdate.cs
     *  The Form that updates the user on the progress of the file splitting
     */
    public partial class ProgressUpdate : Form
    {
        //@{
        /*! Calls the method in the designer class to render the form */
        public ProgressUpdate()
        {
            InitializeComponent();
        }
        //@}
        //! property that sets or gets the current forms title text
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
        //! gets or sets the text of the label used to store current overall progress
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
        //! gets or sets the value of the progressbar's progress
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
        //! gets or sets the maximum value of the progress bar
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
        //! boolean that if set to true will prompt the form to be disposed by the main form
        public bool cancel 
        {
            get 
            {
                return cancel;
            }
            private set 
            {
                cancel = value;
            }
        }
        private void ProgressUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            cancel = true;
        }
    }
}
