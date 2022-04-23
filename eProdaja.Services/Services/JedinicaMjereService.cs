using AutoMapper;
using eProdaja.Controllers;
using eProdaja.Model;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class JedinicaMjereService : BaseService<Model.JediniceMjere, Database.JediniceMjere, JedinicaMjereSearchObject>, //IService<Model.JediniceMjere> 
    //why object? because at this moment we dont care about filtering unit of measures
        IJedinicaMjereService
    {
        public JedinicaMjereService(eProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IQueryable<Database.JediniceMjere> AddFilter(IQueryable<Database.JediniceMjere> query, JedinicaMjereSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(s => s.Naziv.Contains(search.Naziv));
            }
            if (search?.JedinicaMjereId.HasValue == true)
            {
                filteredQuery = filteredQuery.Where(x => x.JedinicaMjereId == search.JedinicaMjereId);
            }

           

            return filteredQuery;
        }
    }
}