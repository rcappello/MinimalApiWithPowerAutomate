using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MinimalApiWithPowerAutomate.API.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Services.Base
{
    public abstract class BaseService
    {
        protected IApplicationDbContext DataContext { get; }

        protected HttpContext HttpContext { get; }

        protected ILogger Logger { get; }

        protected readonly IMapper mapper;

        //protected Guid? UserId => HttpContext?.User.GetId();

        public BaseService(IApplicationDbContext dataContext, 
                            IHttpContextAccessor httpContextAccessor, 
                            ILogger logger, 
                            IMapper mapper)
        {
            DataContext = dataContext;
            HttpContext = httpContextAccessor.HttpContext;
            Logger = logger;
            this.mapper = mapper;
        }
    }
}
