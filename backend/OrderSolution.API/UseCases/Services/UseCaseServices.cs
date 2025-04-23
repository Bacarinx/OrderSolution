using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;
using OrderSolutions.Exception.obj;

namespace OrderSolution.API.UseCases.Services
{
    public class UseCaseServices
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly OrderSolutionDbContext _context;
        public UseCaseServices(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _context = context;
        }

        public ResponseService StartService()
        {
            var userLogged = new LoggedUserService(_httpContext);
            var user = userLogged.getUser(_context);

            if (user == null || user.Id == 0)
                throw new ExceptionUserUnathorized();

            var service = new Entities.Service
            {
                StartService = DateTime.UtcNow,
                UserId = user.Id
            };

            var alreadyExistsServiceOpens = _context.Services.FirstOrDefault(s => s.UserId == user.Id && s.EndService == null);
            if (alreadyExistsServiceOpens != null)
                throw new ExceptionAlreadyServicesOpen();

            _context.Services.Add(service);
            _context.SaveChanges();

            return new ResponseService
            {
                StartService = DateTime.UtcNow,
                UserId = user.Id
            };
        }

        public ResponseService EndService(int serviceid)
        {
            var userLogged = new LoggedUserService(_httpContext);
            var user = userLogged.getUser(_context);

            var service = _context.Services.FirstOrDefault(s => s.Id == serviceid)
                ?? throw new ExceptionServiceNotFound();

            if (user == null || user.Id != service.UserId || user.Id == 0)
                throw new ExceptionUserUnathorized();

            if (service.EndService != null)
                throw new ExceptionServiceAlreadyEnds();

            service.StartService = service.StartService;
            service.UserId = user.Id;
            service.EndService = DateTime.UtcNow;

            _context.SaveChanges();

            return new ResponseService
            {
                StartService = service.StartService,
                EndService = DateTime.UtcNow,
                UserId = user.Id
            };
        }
    }
}