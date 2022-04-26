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
            CurrentEntity.StateMachine = "draft";
        }
        public override void Activate()
        {
            //call entity framework to persist data
            CurrentEntity.StateMachine = "active";
        }
    }
}
