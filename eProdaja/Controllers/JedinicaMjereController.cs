using eProdaja.Services;
using eProdaja.Model;
using Microsoft.AspNetCore.Mvc;
using eProdaja.Model.SearchObjects;
using eProdaja.Model.Requests;

namespace eProdaja.Controllers
{
    public class JedinicaMjereController : BaseCRUDController<Model.JediniceMjere, JedinicaMjereSearchObject, JediniceMjereUpsertRequest, JediniceMjereUpsertRequest>
    {
        public JedinicaMjereController(IJedinicaMjereService service)
            : base(service)
        {
        }
    }
}
