using eProdaja.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProdaja.Services.ProductStateMachine
{
    public abstract class BaseState
    {
        public string CurrentState { get; set; }
        public Database.Proizvodi CurrentEntity { get; set; }

        virtual public void Insert(ProizvodiInsertRequest request)
        { throw new Exception("Not allowed"); }
        virtual public void Update(ProizvodiInsertRequest request)
        { throw new Exception("Not allowed"); }
        virtual public void Activate(ProizvodiInsertRequest request)
        { throw new Exception("Not allowed"); }
        virtual public void Hide(ProizvodiInsertRequest request)
        { throw new Exception("Not allowed"); }
        virtual public void Delete(ProizvodiInsertRequest request)
        { throw new Exception("Not allowed"); }
    }
}
