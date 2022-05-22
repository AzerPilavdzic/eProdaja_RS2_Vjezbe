using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eProdaja.WinUI
{
    public partial class MDIMain : Form
    {
        private int childFormNumber = 0;

        public MDIMain()
        {
            InitializeComponent();
        }

        private void korisniciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pretragaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers childForm = new frmUsers();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.WindowState= FormWindowState.Maximized;
            childForm.Show();
        }

        private void noviKorisnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails childForm = new frmUserDetails();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }
    }
}
