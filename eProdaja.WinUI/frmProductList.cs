using eProdaja.Model;
using Flurl.Http;
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
    public partial class frmProductList : Form
    {
        public APIService ProductService { get; set; } = new APIService("Proizvodi");
       
        public frmProductList()
        {
            InitializeComponent();
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            //call api
            //var list = "https://localhost:7151/Proizvodi".GetJsonAsync<dynamic>().Result;
            var list =await ProductService.Get<List<Proizvodi>>();

            var entity =await ProductService.GetById<Proizvodi>(2003);

            entity.Naziv = "Updated product WinUI";
            var updated =await ProductService.Put<Proizvodi>(entity.ProizvodId,entity);
        }
    }
}
