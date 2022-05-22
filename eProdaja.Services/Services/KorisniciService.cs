using AutoMapper;
using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.Model.SearchObjects;
using eProdaja.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services
{
    public class KorisniciService : BaseCRUDService<Model.Korisnici, Database.Korisnici, KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>, IKorisniciService
    {
        public KorisniciService(eProdajaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public static string GenerateSalt()
        {
            return Convert.ToBase64String(new byte[16]);

        }

        public override Model.Korisnici Insert(KorisniciInsertRequest insert)
        {
            if (insert.Password!=insert.PasswordConfirmation)
            {
                throw new UserException("Passwords must be same!");
            }

            var entity = base.Insert(insert);

            foreach (var ulogaId in insert.UlogeIdList)
            {
                Database.KorisniciUloge korisniciUloge = new Database.KorisniciUloge();
                korisniciUloge.UlogaId = ulogaId;
                korisniciUloge.KorisnikId = entity.KorisnikId;
                korisniciUloge.DatumIzmjene = DateTime.Now;

                _context.KorisniciUloges.Add(korisniciUloge);
            }
            _context.SaveChanges();
            return entity;
        }

        public override void BeforeInsert(KorisniciInsertRequest insert, Database.Korisnici entity)
        {
            var salt = GenerateSalt();
            entity.LozinkaSalt = salt;
            entity.LozinkaHash = GenerateHash(salt, insert.Password);
            base.BeforeInsert(insert, entity);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dest = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dest, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dest, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dest);

            return Convert.ToBase64String(inArray);
        }
        public override IQueryable<Database.Korisnici> AddFilter(IQueryable<Database.Korisnici> query, KorisniciSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.KorisnickoIme))
            {
                filteredQuery = filteredQuery.Where(x => x.KorisnickoIme.Contains(search.KorisnickoIme));
            }

            if (!string.IsNullOrWhiteSpace(search?.NameFTS))
            {
                filteredQuery = filteredQuery.Where(x => x.KorisnickoIme.Contains(search.NameFTS)
                    || x.Ime.Contains(search.NameFTS)
                    || x.Prezime.Contains(search.NameFTS));
            }

            return filteredQuery;
            return base.AddFilter(query, search);

        }
        public override IQueryable<Database.Korisnici> AddInclude(IQueryable<Database.Korisnici> query, KorisniciSearchObject search = null)
        {
            if (search?.IncludeRoles == true)
            {
                query = query.Include("KorisniciUloges.Uloga");
            }
            return base.AddInclude(query, search);
        }

        public Model.Korisnici Login(string username, string password)
        {
            var entity = _context.Korisnicis.Include("KorisniciUloges.Uloga").FirstOrDefault(x => x.KorisnickoIme == username);
            if (entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.LozinkaSalt, password);
            if (hash != entity.LozinkaHash)
            {
                return null;
            }
            return _mapper.Map<Model.Korisnici>(entity);

        }
    }
}
