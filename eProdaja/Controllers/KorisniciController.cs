using eProdaja.Services;
using eProdaja.Model;
using Microsoft.AspNetCore.Mvc;
using eProdaja.Model.SearchObjects;
using eProdaja.Model.Requests;
using Microsoft.AspNetCore.Authorization;

namespace eProdaja.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //we can remove line 13 because we have added annotation in BaseController
    [Authorize]
    public class KorisniciController : BaseCRUDController<Model.Korisnici, KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>
    {
        public KorisniciController(IKorisniciService service)
            : base(service)
        {
        }
        //[Authorize("Administrator")]
        public override Korisnici Insert(KorisniciInsertRequest insert)
        {
            return base.Insert(insert);
        }
        
        //[Authorize("Administrator")]
        public override Korisnici Update(int id, [FromBody] KorisniciUpdateRequest update)
        {
            return base.Update(id, update);
        }
    }
}
