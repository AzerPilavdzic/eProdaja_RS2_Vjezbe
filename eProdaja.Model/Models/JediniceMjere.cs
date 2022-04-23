using System;

namespace eProdaja.Model
{
    public partial class JediniceMjere
    {
        public int JedinicaMjereId { get; set; }
        public string Naziv { get; set; }

        //public virtual ICollection<Izlazi> Izlazis { get; set; }
        //public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; }
        //public virtual ICollection<Ulazi> Ulazis { get; set; }
    }
}
