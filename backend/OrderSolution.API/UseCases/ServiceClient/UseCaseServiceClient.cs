using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.ServiceClient
{
    public class UseCaseServiceClient
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

#pragma warning disable IDE0290
        public UseCaseServiceClient(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public ResponseClientServiceAdded AddClientInService(int serviceid, int clientid)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientid);
            var service = _context.Services.FirstOrDefault(s => s.Id == clientid);

            var nullMiddleware = new NullMiddlaware();

            nullMiddleware.Execute(service, "Serviço");
            nullMiddleware.Execute(client, "Cliente");

            if (service.EndService != null)
                throw new ExceptionClientServices(["Esse serviço já fechou! Inicie um outro serviço para incluir usuário"]);

            var clientAlreadyInService = _context.ServiceClients.FirstOrDefault(c => c.ClientId == clientid && c.ServiceId == serviceid);
            if (clientAlreadyInService != null)
                throw new ExceptionClientServices(["Usuário já está cadastrado no serviço!"]);

            var LoggedUser = new LoggedUserService(_httpContext);
            var user = LoggedUser.getUser(_context);

            var serviceClient = new Entities.ServiceClient
            {
                ClientId = client.Id,
                ServiceId = service.Id
            };

            _context.ServiceClients.Add(serviceClient);
            _context.SaveChanges();

            return new ResponseClientServiceAdded
            {
                UserId = serviceClient.ClientId,
                ServiceId = serviceClient.ServiceId
            };
        }
    }
}