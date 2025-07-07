using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Tab
{
    public class UseCaseTab
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly UserMiddlaware _middlaware;
        private readonly Entities.User? User;
        private const int STEP = 64;

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
                IsOpen = false,
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

        public ResponseGetTabs GetTabs(int pagenumber, string code)
        {
            var query = _context.Tab.AsQueryable();
            query = query.Where(t => t.UserId == User!.Id);

            if (!String.IsNullOrWhiteSpace(code))
            {
                query = query.Where(t => t.Code.Contains(code));
            }

            var total = query.Count();

            if (pagenumber != 0)
            {
                query = query.OrderBy(c => c.Code)
                              .Skip(STEP * (pagenumber - 1))
                              .Take(STEP);
            }

            List<ResponseTab> tabs = [];

            var queryList = query.OrderBy(c => c.Code).ToList();

            foreach (var tab in queryList)
            {
                var client = _context.Clients.FirstOrDefault(c => c.Id == tab.ClientId);
                decimal tabValue = 0;

                var OpenItensTab = _context.TabProducts.Where(tp => tp.IsPaid == false && tp.TabId == tab.Id).ToList();
                foreach (var i in OpenItensTab)
                {
                    if (i.IsActive == true)
                        tabValue += _context.Products.FirstOrDefault(a => a.Id == i.ProductId)!.Price;
                }

                tabs.Add(
                    new ResponseTab
                    {
                        TabId = tab.Id,
                        Code = tab.Code,
                        UserId = User!.Id,
                        ClientName = client?.Name,
                        ClientCPF = client?.CPF,
                        Value = tabValue,
                        IsOpen = tab.IsOpen
                    }
                );
            }

            return new ResponseGetTabs
            {
                Tabs = tabs,
                Qtd = total
            };
        }

        public ResponseDescribeTab GetTab(string code)
        {
            var tab = _context.Tab.FirstOrDefault(t => t.Code == code && t.UserId == User!.Id);
            decimal tabValue = 0;
            var client = _context.Clients.FirstOrDefault(c => c.Id == tab!.ClientId);
            var OpenItensTab = _context.TabProducts.Where(tp => tp.IsPaid == false && tp.TabId == tab!.Id).ToList();

            List<ResponseProductsOnTab> products = [];

            foreach (var i in OpenItensTab)
            {
                var produto = _context.Products.FirstOrDefault(a => a.Id == i.ProductId);

                if (i.IsActive == true)
                {
                    tabValue += produto!.Price;
                }

                products.Add(new ResponseProductsOnTab
                {
                    TabProductId = i.Id,
                    Value = produto!.Price,
                    ProductName = produto.Name,
                    InsertionDate = i.InsertionDate,
                    IsActive = i.IsActive
                });
            }

            return new ResponseDescribeTab
            {
                TabId = tab!.Id,
                Code = tab.Code,
                ClientName = client?.Name,
                ClientCPF = client?.CPF,
                IsOpen = tab.IsOpen,
                Value = tabValue,
                Products = products
            };
        }
    }
}