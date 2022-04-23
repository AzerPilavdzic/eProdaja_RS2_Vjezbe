using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;

namespace eProdaja.Controllers
{
    public interface IJedinicaMjereService : IService<Model.JediniceMjere, JedinicaMjereSearchObject>
    {
    }
}