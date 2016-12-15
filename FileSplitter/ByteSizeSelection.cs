using System;
using System.Windows.Forms;

namespace FileSplitter
{
    /*! \file ByteSizeSelection.cs
     *  Form that will prompt the user what byte size they desire if they choose none, then assume the number entered into Form1.cs textbox is the literal size
     */
    public partial class ByteSizeSelection : Form
    {
        //@{
        /*! Creates the Form by calling the method in the ByteSizeSelection designer */
        public ByteSizeSelection()
        {
            InitializeComponent();
        }
        //@}
        //@{
        /*! stores the value of whether GigaByte is selected or not */
        public bool GigaByte_Selected
        {
            get
            {
                return GigaByte_Selected;
            }
            set
            {
                GigaByte_Selected = value;
            }
        }
        //@}
        //@{
        /*! stores the value of whether MegaByte is selected or not*/
        public bool MegaByte_Selected
        {
            get
            {
                return MegaByte_Selected;
            }
            set
            {
                MegaByte_Selected = value;
            }
        }
        //@}
        //@{
        /*! stores the value of whether KiloByte is selected or not */
        public bool KiloByte_Selected
        {
            get
            {
                return KiloByte_Selected;
            }
            set
            {
                KiloByte_Selected = value;
            }
        }
        //@}
        //@{
        /*! stores the value of whether None is selected or not*/
        public bool None_Selected
        {
            get
            {
                return None_Selected;
            }
            set
            {
                None_Selected = value;
            }
        }
        //@}
        //@{
        /*! Event that is called when the radiobutton check state is changed */
        private void GigaByte_CheckedChanged(object sender, EventArgs e)
        {
            GigaByte_Selected = GigaByte.Checked;
            MegaByte_Selected = MegaBytes.Checked;
            KiloByte_Selected = KiloByte.Checked;
            None_Selected = Literal.Checked;
        }
        //@}
        //@{
        /*! Event that is called when the radiobutton check state is changed */
        private void MegaBytes_CheckedChanged(object sender, EventArgs e)
        {
            GigaByte_Selected = GigaByte.Checked;
            MegaByte_Selected = MegaBytes.Checked;
            KiloByte_Selected = KiloByte.Checked;
            None_Selected = Literal.Checked;
        }
        //@}
        //@{
        /*! Event that is called when the radiobutton check state is changed */
        private void KiloByte_CheckedChanged(object sender, EventArgs e)
        {
            GigaByte_Selected = GigaByte.Checked;
            MegaByte_Selected = MegaBytes.Checked;
            KiloByte_Selected = KiloByte.Checked;
            None_Selected = Literal.Checked;
        }
        //@}
        //@{
        /*! Event that is called when the radiobutton check state is changed */
        private void Literal_CheckedChanged(object sender, EventArgs e)
        {
            GigaByte_Selected = GigaByte.Checked;
            MegaByte_Selected = MegaBytes.Checked;
            KiloByte_Selected = KiloByte.Checked;
            None_Selected = Literal.Checked;
        }
        //@}
        private void ByteSizeSelection_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
