using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly UserMiddlaware Middlaware;

#pragma warning disable IDE0290
        public UseCaseServiceClient(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            Middlaware = new UserMiddlaware();
        }

        public ResponseClientServiceAdded AddClientInService(int serviceid, int clientid)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientid);
            var service = _context.Services.FirstOrDefault(s => s.Id == serviceid);

            Middlaware.NullMid(service, "Serviço");
            Middlaware.NullMid(client, "Cliente");

            if (service!.EndService != null)
                throw new ExceptionClientServices(["Esse serviço já fechou! Inicie um outro serviço para incluir usuário"]);

            var clientAlreadyInService = _context.ServiceClients.FirstOrDefault(c => c.ClientId == clientid && c.ServiceId == serviceid);
            if (clientAlreadyInService != null)
                throw new ExceptionClientServices(["Usuário já está cadastrado no serviço!"]);

            var LoggedUser = new LoggedUserService(_httpContext);
            var user = LoggedUser.GetUser(_context);

            var serviceClient = new Entities.ServiceClient
            {
                ClientId = client!.Id,
                ServiceId = service.Id,
                UserId = user.Id
            };

            _context.ServiceClients.Add(serviceClient);
            _context.SaveChanges();

            return new ResponseClientServiceAdded
            {
                UserId = serviceClient.ClientId,
                ServiceId = serviceClient.ServiceId
            };
        }

        public void RemoveClientFromService(int serviceClientId)
        {
            var serviceClient = _context.ServiceClients.FirstOrDefault(s => s.Id == serviceClientId);
            Middlaware.NullMid(serviceClient, "cliente");

            var LoggedUser = new LoggedUserService(_httpContext);
            var user = LoggedUser.GetUser(_context);
            Middlaware.UserMid(user, serviceClient);

            _context.ServiceClients.Remove(serviceClient!);
            _context.SaveChanges();
        }

        public ResponseServiceClients GetServiceClients(int serviceId)
        {
            var serviceClients = _context.ServiceClients.Where(sc => sc.ServiceId == serviceId).ToList();
            List<ResponseClient> clients = [];

            if (serviceClients.Count > 0)
            {
                foreach (var i in serviceClients)
                {
                    var client = _context.Clients.FirstOrDefault(c => c.Id == i.ClientId);
                    clients.Add(
                        new ResponseClient
                        {
                            ClientId = client!.Id,
                            CPF = client.CPF,
                            Email = client.Email,
                            Gender = client.Gender,
                            Name = client.Name,
                            PhoneNumber = client.PhoneNumber
                        }
                    );
                }
            }

            return new ResponseServiceClients
            {
                ServiceId = serviceId,
                Clients = clients
            };
        }
    }
}