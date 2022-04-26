using AutoMapper;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using eProdaja.Services.ProductStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    //public class ProizvodiService : BaseService<Model.Proizvodi, Database.Proizvodi, ProizvodiSearchObject>, IProizvodiService
    public class ProizvodiService : BaseCRUDService<Model.Proizvodi, Database.Proizvodi, ProizvodiSearchObject, ProizvodiInsertRequest, ProizvodiUpdateRequest>, IProizvodiService
    {
        public BaseState BaseState { get; set; }
        public ProizvodiService(eProdajaContext context, IMapper mapper, BaseState baseState)
            : base(context, mapper)
        {
            BaseState = baseState;
        }

        public override Model.Proizvodi Insert(ProizvodiInsertRequest insert)
        {
            //metodu smo proglasili "static" iz proguglat cemo kojeg razloga
            var state = BaseState.CreateState("initial");
            //state._context = _context;

            return state.Insert(insert);
        }


        public override Model.Proizvodi Update(int id, ProizvodiUpdateRequest update)
        {
            var product = _context.Proizvodis.Find(id);

            var state = BaseState.CreateState(product.StateMachine);
            //state._context = _context;
            state.CurrentEntity = product;

            state.Update(update);

            return GetById(id);
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


        public Model.Proizvodi Activate(int id)
        {
            var product = _context.Proizvodis.Find(id);

            var state = BaseState.CreateState(product.StateMachine);

            state.Context= _context;
            state.CurrentEntity = product;

            state.Activate();

            return GetById(id);

        }

        //public IEnumerable<Model.Proizvodi> Get(ProizvodiSearchObject proizvodiSearchObject)
        //{

        //}
    }
}