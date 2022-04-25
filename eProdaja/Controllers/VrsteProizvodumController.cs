using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using eProdaja.Services.Interfaces;

namespace eProdaja.Controllers
{
    public class VrsteProizvodumController : BaseCRUDController<Model.VrsteProizvodum, VrsteProizvodumSearchObject, VrsteProizvodumUpsertRequest, VrsteProizvodumUpsertRequest>
    {
        public VrsteProizvodumController(IVrsteProizvodumService service)
            : base(service)
        {

        }
    }
}
