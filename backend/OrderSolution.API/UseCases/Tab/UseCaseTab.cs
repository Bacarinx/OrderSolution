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
        private readonly IHttpContextAccessor _httpcontext;
        private readonly UserMiddlaware _middlaware;
        private readonly Entities.User? User;
        public UseCaseTab(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;

            var loggedUser = new LoggedUserService(_httpcontext);
            User = loggedUser.GetUser(_context);
            _middlaware = new UserMiddlaware();
        }

        public string CreateTab(RequestNewTab request)
        {
            if (String.IsNullOrWhiteSpace(request.Code))
                throw new ExceptionNullObject("Por favor, digíte um código válido para a comanda");

            var tab = _context.Tab.FirstOrDefault(t => t.Code == request.Code && t.UserId == User!.Id);
            if (tab != null)
            {
                throw new ExceptionNullObject("Essa código já existe para outra comanda");
            }

            var newtab = new Entities.Tab
            {
                UserId = User!.Id,
                Code = request.Code,
                ClientId = null
            };

            _context.Tab.Add(newtab);
            _context.SaveChanges();
            return request.Code;
        }

        public void RemoveTab(int tabId)
        {
            var tab = _context.Tab.FirstOrDefault(t => t.Id == tabId);
            _middlaware.NullMid(tab, "Comanda");
            _middlaware.UserMid(User!, tab);

            _context.Tab.Remove(tab!);
            _context.SaveChanges();
        }

        public void TrocarPessoaComanda(int id, RequestTabChangeClient request)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == request.clientId);
            var tab = _context.Tab.FirstOrDefault(f => f.Id == id);

            _middlaware.NullMid(client, "Cliente");
            _middlaware.NullMid(tab, "Comanda");
            _middlaware.UserMid(User!, tab);

            var PendentsProductsAmount = _context.TabProducts.Count(tp => tp.TabId == tab!.Id && tp.IsPaid == false);
            if (PendentsProductsAmount > 0)
                throw new ExceptionClientServices(["Você não pode substituir o cliente desta comanda! Há itens abertos a serem pagos."]);

            tab!.ClientId = request.clientId;
            _context.SaveChanges();
        }
    }
}