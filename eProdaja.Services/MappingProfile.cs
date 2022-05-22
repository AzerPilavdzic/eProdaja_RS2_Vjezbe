using AutoMapper;
using eProdaja.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Korisnici, Model.Korisnici>();

            //mapiranje u liniji 18 i 19 smo dodali kada smo pravili login sa 
            //auth.
            CreateMap<Database.KorisniciUloge, Model.KorisniciUloge>();
            CreateMap<Database.Uloge, Model.Uloge>();


            CreateMap<Database.JediniceMjere, Model.JediniceMjere>();
            CreateMap<Database.Proizvodi, Model.Proizvodi>();
            CreateMap<Database.VrsteProizvodum, Model.VrsteProizvodum>();

            CreateMap<JediniceMjereUpsertRequest, Database.JediniceMjere>();

            CreateMap<ProizvodiInsertRequest, Database.Proizvodi>();
            CreateMap<ProizvodiUpdateRequest, Database.Proizvodi>();

            CreateMap<VrsteProizvodumUpsertRequest, Database.VrsteProizvodum>();

            CreateMap<KorisniciInsertRequest, Database.Korisnici>();
            CreateMap<KorisniciUpdateRequest, Database.Korisnici>();



        }
    }
}
