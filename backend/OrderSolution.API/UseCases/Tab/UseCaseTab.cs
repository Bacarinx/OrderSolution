using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Requests;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Tab
{
    public class UseCaseTab
    {
        private readonly OrderSolutionDbContext _context;
        private readonly HttpContextAccessor _httpcontext;
        private readonly UserMiddlaware _middlaware;
        private readonly Entities.User? User;
        public UseCaseTab(OrderSolutionDbContext context, HttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;

            var loggedUser = new LoggedUserService(_httpcontext);
            Entities.User User = loggedUser.GetUser(_context);
            _middlaware = new UserMiddlaware();
        }

        public string CreateTab(string request)
        {
            if (String.IsNullOrWhiteSpace(request))
                throw new ExceptionNullObject("Por favor, digíte um código válido para a comanda");

            var tab = new Entities.Tab
            {
                UserId = User!.Id,
                ClientId = 0 // if it's be 0, then there's no client with tab
            };

            _context.Tab.Add(tab);
            _context.SaveChanges();
            return (request);
        }

        public void RemoveTab(int tabId)
        {
            var tab = _context.Tab.FirstOrDefault(t => t.Id == tabId);
            _middlaware.NullMid(tab, "Comanda");
            _middlaware.UserMid(User!, tab);

            _context.Tab.Remove(tab!);
            _context.SaveChanges();
        }

        public void TrocarPessoaComanda(RequestTabChangeClient request)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == request.clientId);
            var tab = _context.Tab.FirstOrDefault(f => f.Id == request.TabId);

            _middlaware.NullMid(client, "Cliente");
            _middlaware.NullMid(tab, "Comanda");
            _middlaware.UserMid(User!, tab);

            tab!.ClientId = request.clientId;
            _context.SaveChanges();
        }
    }
}