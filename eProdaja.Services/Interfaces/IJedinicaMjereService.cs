using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;

namespace eProdaja.Controllers
{
    public interface IJedinicaMjereService : ICRUDService<Model.JediniceMjere, JedinicaMjereSearchObject,JediniceMjereUpsertRequest, JediniceMjereUpsertRequest>
    {
    }
}