using AutoMapper;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : IKorisniciService
    {
        public eProdajaContext _context { get; set; }
        public IMapper Mapper { get; set; }

        public KorisniciService(eProdajaContext context, IMapper mapper)
        {
            _context = context;
            Mapper = mapper;
        }

        public IEnumerable<Model.Korisnici> Get()
        {
            var result = _context.Korisnicis.ToList();
            return Mapper.Map<List<Model.Korisnici>>(result);
        }
    }
}
