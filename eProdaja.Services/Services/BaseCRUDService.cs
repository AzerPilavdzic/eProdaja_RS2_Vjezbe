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
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate>
        : BaseService<T, TDb, TSearch>, ICRUDService<T, TSearch, TInsert, TUpdate>
        where T : class where TDb : class where TSearch
        : BaseSearchObject where TInsert : class where TUpdate : class
    {
        public BaseCRUDService(eProdajaContext context, IMapper mapper)
            : base(context, mapper) { }

        virtual public T Insert(TInsert insert)
        {
            //entity set
            var set = _context.Set<TDb>();
            TDb entity = _mapper.Map<TDb>(insert);
            set.Add(entity);
            BeforeInsert(insert, entity);
            _context.SaveChanges();

            return _mapper.Map<T>(entity);

        }

        virtual public void BeforeInsert(TInsert insert, TDb entity)
        {

        }

        virtual public T Update(int id, TUpdate update)
        {
            var set = _context.Set<TDb>();
            var entity = set.Find(id);

            if (entity != null)
            {
                _mapper.Map(update, entity);
            }
            else
            {
                return null;
            }
            //move these two lines up?
            _context.SaveChanges();
            return _mapper.Map<T>(entity);
        }
    }
}
