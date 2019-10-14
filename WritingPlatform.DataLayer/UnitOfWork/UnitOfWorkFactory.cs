using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WritingPlatform.DataLayer.UnitOfWork
{
    public class UnitOfWorkFactory
    {
        public virtual IUnitOfWork Create()
        {
            return new UnitOfWork(new Model1());
        }
    }
}
