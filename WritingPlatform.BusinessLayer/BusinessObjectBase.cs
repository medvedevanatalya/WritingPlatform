using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WritingPlatform.DataLayer.UnitOfWork;

namespace WritingPlatform.BusinessLayer
{
    public abstract class BusinessObjectBase
    {
        protected IMapper mapper;
        protected UnitOfWorkFactory unitOfWorkFactory;

        public BusinessObjectBase(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}
