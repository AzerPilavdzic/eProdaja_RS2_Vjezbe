using System;
using System.Collections.Generic;
using System.Text;

namespace eProdaja.Model.SearchObjects
{
    public class JedinicaMjereSearchObject : BaseSearchObject
    {
        public string Naziv { get; set; }
        public int ? JedinicaMjereId { get; set; }
    }
}
