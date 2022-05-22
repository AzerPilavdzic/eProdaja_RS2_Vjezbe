using eProdaja.Services;
using eProdaja.Model;
using Microsoft.AspNetCore.Mvc;
using eProdaja.Model.SearchObjects;
using eProdaja.Model.Requests;
using Microsoft.AspNetCore.Authorization;

namespace eProdaja.Controllers
{
    [AllowAnonymous]
    public class UlogeController : BaseController<Model.Uloge, BaseSearchObject>
    {
        //BaseSearchObject
        public UlogeController(IService<Model.Uloge, BaseSearchObject> service)
            : base(service)
        {
        }


    }
}
