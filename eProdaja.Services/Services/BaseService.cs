using AutoMapper;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch> where T : class where TDb : class where TSearch : BaseSearchObject
    {
        public eProdajaContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public BaseService(eProdajaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual IEnumerable<T> Get(TSearch search = null)
        {
            //TDb je DataSet?
            var entity = _context.Set<TDb>().AsQueryable();

            entity = AddFilter(entity, search);

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                entity = entity.Take(search.PageSize.Value).Skip(search.Page.Value * search.PageSize.Value);
            }

            var list = entity.ToList();
            return _mapper.Map<IList<T>>(list);
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch search = null)
        {
            return query;
        }

        public T GetById(int id)
        {
            //"set" is a variable and with that var we know which class is about to be "shaked"
            var set = _context.Set<TDb>();
            // "entity" will be an object from DataBase which is found by forwarded ID
            var entity = set.Find(id);

            return _mapper.Map<T>(entity);
        }
    }
}
