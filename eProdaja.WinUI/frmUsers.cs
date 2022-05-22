using eProdaja.Model;
using eProdaja.Model.SearchObjects;
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
    public partial class frmUsers : Form
    {
        public APIService UsersService { get; set; } = new APIService("Korisnici");


        public frmUsers()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {

        }

        private async void btnShowUsers_Click(object sender, EventArgs e)
        {
            var searchObject= new KorisniciSearchObject();
            searchObject.KorisnickoIme = tbUsername.Text;
            searchObject.NameFTS= tbName.Text;
            searchObject.IncludeRoles = true;
            var _list = await UsersService.Get<List<Korisnici>>(searchObject);
            dataGridView1.DataSource= _list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var objekat = dataGridView1.SelectedRows[0].DataBoundItem as Korisnici;
            frmUserDetails frmUserDetails = new frmUserDetails(objekat);
            frmUserDetails.ShowDialog();

        }
    }
}
