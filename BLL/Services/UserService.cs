using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : Service<BllUser, DalUser, User, UserMapper>, IUserService
    {
       // private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow) : base(uow, uow.Users)
        {
        //    this.uow = uow;
         //   mapper = new UserMapper(uow);
        }

        protected override void InitMapper()
        {
            mapper = new UserMapper(uow);
        }

        public BllUser Authorize(string login, string password)
        {
            var user = uow.Users.Authorize(login, password);
            if (user != null)
            {
                return mapper.MapToBll(user);
            }
            return null;
        }

        public BllUser GetUserByLogin(string login)
        {
            return mapper.MapToBll(uow.Users.GetUserByLogin(login));
        }

        public new BllUser Create(BllUser entity)
        {
            var testEntity = uow.Users.GetUserByLogin(entity.Login);
            if (testEntity == null)
            {
                uow.Users.Create(mapper.MapToDal(entity));
                uow.Commit();
                return entity;
            }
            return null;
        }
    }
}
