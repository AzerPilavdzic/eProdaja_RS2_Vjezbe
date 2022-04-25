using eProdaja.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProductStateMachine
{
    public class DraftProductState : BaseState
    {
        public override void Update(ProizvodiInsertRequest request)
        {
            //call entity framework to persist data
            CurrentEntity.StateMachine = "draft";
        }
        public override void Activate(ProizvodiInsertRequest request)
        {
            //call entity framework to persist data
            CurrentEntity.StateMachine = "active";
        }
    }
}
