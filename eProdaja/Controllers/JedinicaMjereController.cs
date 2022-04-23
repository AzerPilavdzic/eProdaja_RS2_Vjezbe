using eProdaja.Services;
using eProdaja.Model;
using Microsoft.AspNetCore.Mvc;
using eProdaja.Model.SearchObjects;

namespace eProdaja.Controllers
{
    public class JedinicaMjereController : BaseController<Model.JediniceMjere, JedinicaMjereSearchObject>
    {
        public JedinicaMjereController(IJedinicaMjereService service)
            : base(service)
        {
        }
    }
}
