using eProdaja.Model;
using eProdaja.Model.Requests;
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
    public partial class frmUserDetails : Form
    {
        public APIService UsersService { get; set; } = new APIService("Korisnici");
        public APIService RoleService { get; set; } = new APIService("Uloge");
        public Korisnici _model = null;
        public frmUserDetails(Korisnici model = null)
        {
            InitializeComponent();
            _model = model;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (ValidateChildren())
            {
                var roleList = checkedListRoles.CheckedItems.Cast<Uloge>().ToList();
                var roleIdList = roleList.Select(x => x.UlogaId).ToList();



                if (_model == null)
                {

                    KorisniciInsertRequest korisniciInsert = new KorisniciInsertRequest()
                    {
                        Ime = tbName.Text,
                        Prezime = tbSurname.Text,
                        Email = tbEmail.Text,
                        KorisnickoIme = tbUsername.Text,
                        Status = cbStatus.Checked,
                        Password = tbPassword.Text,
                        PasswordConfirmation = tbPasswordConfirmation.Text,
                        UlogeIdList = roleIdList
                    };

                    var user = await UsersService.Post<Korisnici>(korisniciInsert);
                }
                else
                {
                    var obj = await UsersService.GetById<Korisnici>(_model.KorisnikId);
                    KorisniciUpdateRequest updateRequest = new KorisniciUpdateRequest()
                    {
                        Ime = tbName.Text,
                        Prezime = tbSurname.Text,
                        Email = tbEmail.Text,
                        Status = cbStatus.Checked,
                        Telefon = "132321",
                        Password = tbPassword.Text,
                        PasswordConfirmation = tbPasswordConfirmation.Text
                    };

                    var user = await UsersService.Put<Korisnici>(_model.KorisnikId, updateRequest);
                }
            }
        }

        private async void frmUserDetails_Load(object sender, EventArgs e)
        {
            //ucitaj uloge sa apija
            await LoadRoles();

            if (_model != null)
            {
                tbName.Text = _model.Ime;
                tbSurname.Text = _model.Prezime;
                tbEmail.Text = _model.Email;
                tbUsername.Text = _model.KorisnickoIme;
                tbPassword.Text = _model.Password;
                tbPasswordConfirmation.Text = _model.PasswordConfirmation;
                cbStatus.Checked = _model.Status.GetValueOrDefault(false);
            }
        }

        private async Task LoadRoles()
        {
            var roles = await RoleService.Get<List<Uloge>>();
            checkedListRoles.DataSource = roles;
            checkedListRoles.DisplayMember = "Naziv";
        }


        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                e.Cancel = true;
                tbName.Focus();
                errorProvider1.SetError(tbName, "Ime ne smije biti prazno!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
                //errorProvider1.SetError(tbName, "");
            }
        }
    }
}
