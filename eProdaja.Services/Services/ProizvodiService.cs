using AutoMapper;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    //public class ProizvodiService : BaseService<Model.Proizvodi, Database.Proizvodi, ProizvodiSearchObject>, IProizvodiService
    public class ProizvodiService : BaseCRUDService<Model.Proizvodi, Database.Proizvodi, ProizvodiSearchObject,ProizvodiInsertRequest, ProizvodiUpdateRequest>, IProizvodiService
    {
        public ProizvodiService(eProdajaContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IQueryable<Proizvodi> AddFilter(IQueryable<Proizvodi> query, ProizvodiSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Sifra))
            {
                filteredQuery = filteredQuery.Where(s => s.Sifra == search.Sifra);
            }
            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(s => s.Naziv.Contains(search.Naziv));
            }

            return filteredQuery;
        }

        public override IEnumerable<Model.Proizvodi> Get(ProizvodiSearchObject proizvodi)
        {
            return base.Get();
        }

        public Model.Proizvodi Update(int id, ProizvodiInsertRequest update)
        {
            throw new NotImplementedException();
        }
        //public IEnumerable<Model.Proizvodi> Get(ProizvodiSearchObject proizvodiSearchObject)
        //{

        //}
    }
}