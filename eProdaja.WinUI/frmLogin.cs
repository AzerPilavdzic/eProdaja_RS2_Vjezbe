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
    public partial class frmLogin : Form
    {
        private readonly APIService _api = new APIService("Korisnici");
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            APIService._username=tbUsername.Text;
            APIService._password = tbPassword.Text;

            try
            {
                var result = await _api.Get<dynamic>();

                //frmUsers frm = new frmUsers();
                //frm.Show();
                MDIMain mDIMain = new MDIMain();
                mDIMain.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong username or password");
            }

        }
    }
}
