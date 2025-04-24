using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Entities;
using OrderSolution.API.Interfaces;
using OrderSolutions.Exception;

namespace OrderSolution.API.Middleware
{
    public class UserMiddlaware
    {
        public void Execute<T>(User user, T? entity) where T : class, IOwnedUserId
        {
            if (entity != null)
            {
                if (user == null || user.Id != entity.UserId || user.Id == 0)
                    throw new ExceptionUserRegister(["Você não possui acesso para isso!"]);
            }
        }
    }
}