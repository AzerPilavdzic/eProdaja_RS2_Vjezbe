using AutoMapper;
using eProdaja.Model.Requests;
using eProdaja.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProductStateMachine
{
    public class DraftProductState : BaseState
    {
        public DraftProductState(IServiceProvider serviceProvider, eProdajaContext context, IMapper mapper) : base(serviceProvider, context, mapper)
        {
        }

        public override void Update(ProizvodiUpdateRequest request)
        {
            //call entity framework to persist data
            var set = Context.Set<Database.Proizvodi>();

            Mapper.Map(request,CurrentEntity);
              Context.SaveChanges();
            CurrentEntity.StateMachine = "draft";
          


        }
        public override void Activate()
        {
            //call entity framework to persist data
            CurrentEntity.StateMachine = "active";
            Context.SaveChanges();
        }
        public override List<string> AllowedActions()
        {
            var list = base.AllowedActions();
            list.Add("update");
            list.Add("activate");
            list.Add("hide");

            return list;
        }
    }
}
