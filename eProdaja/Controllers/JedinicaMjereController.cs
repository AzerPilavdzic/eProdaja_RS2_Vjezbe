using eProdaja.Services;
using eProdaja.Model;
using Microsoft.AspNetCore.Mvc;
using eProdaja.Model.SearchObjects;
using eProdaja.Model.Requests;
using Microsoft.AspNetCore.Authorization;

namespace eProdaja.Controllers
{
    public class JedinicaMjereController : BaseCRUDController<Model.JediniceMjere, JedinicaMjereSearchObject, JediniceMjereUpsertRequest, JediniceMjereUpsertRequest>
    {
        public JedinicaMjereController(IJedinicaMjereService service)
            : base(service)
        {
        }

        [AllowAnonymous]
        public override IEnumerable<JediniceMjere> Get([FromQuery] JedinicaMjereSearchObject search = null)
        {
            return base.Get(search);
        }

        [AllowAnonymous]
        public override JediniceMjere GetById(int id)
        {
            return base.GetById(id);
        }


    }
}
