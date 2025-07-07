using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Core.Domain.Entities
{
    public abstract class Base
    {
        public virtual int Id { get; set; }
        public virtual void Atualizar(object obj) { }
    }
}
