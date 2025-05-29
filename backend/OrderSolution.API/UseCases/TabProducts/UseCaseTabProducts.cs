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

namespace OrderSolution.API.UseCases.TabProducts
{
    public class UseCaseTabProducts
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly UserMiddlaware _middlaware;
        private readonly Entities.User _User;
        public UseCaseTabProducts(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
            _context = context;

            _middlaware = new UserMiddlaware();
            var LoggedUser = new LoggedUserService(_httpcontext);
            _User = LoggedUser.GetUser(_context);
        }

        public List<ResponseTabProductsAdd> AddProductToTab(List<RequestTabProductsAdd> requestList)
        {
            List<ResponseTabProductsAdd> lista = [];
            foreach (var req in requestList)
            {
                var tab = _context.Tab.FirstOrDefault(t => t.Id == req.TabId);
                var prod = _context.Products.FirstOrDefault(p => p.Id == req.ProductId);

                _middlaware.NullMid(tab, "Comanda");
                _middlaware.NullMid(prod, "Produto");

                var client = _context.Clients.FirstOrDefault(c => c.Id == tab!.ClientId);
                _middlaware.NullMid(client, "Cliente");

                var prodToBeAdd = new Entities.TabProducts
                {
                    TabId = tab!.Id,
                    ProductId = prod!.Id,
                    UserId = _User.Id,
                    ClientId = client!.Id,
                    InsertionDate = DateTime.UtcNow,
                    IsActive = true,
                    IsPaid = false,
                    PaymentDate = null
                };

                lista.Add(new ResponseTabProductsAdd
                {
                    Product = prod.Name,
                    Tab = tab.Code,
                    Time = DateTime.UtcNow.ToLocalTime()
                });

                _context.TabProducts.Add(prodToBeAdd);

                tab = null;
                prod = null;
            }

            _context.SaveChanges();

            return lista;
        }

        public string CancelProduct(int TabProdId)
        {
            var prodTab = _context.TabProducts.FirstOrDefault(tp => tp.Id == TabProdId);
            _middlaware.NullMid(prodTab, "O produto");
            _middlaware.UserMid(_User, prodTab);

            if (prodTab!.IsActive == false)
                throw new ExceptionClientServices(["O produto já foi cancelado."]);

            prodTab.IsActive = false;

            return "Produto Cancelado";
        }

        public void TabPayment(int tabId)
        {
            var tab = _context.Tab.FirstOrDefault(tp => tp.Id == tabId);
            _middlaware.NullMid(tab, "Comanda");
            _middlaware.UserMid(_User, tab);

            var listProd = _context.TabProducts.Where(tp => tp.TabId == tabId).ToList();

            foreach (var l in listProd)
            {
                l.IsPaid = true;
            }

            _context.SaveChanges();
        }
    }
}