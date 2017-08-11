using BLL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IControlMethodsLibService : IService<BllControlMethodsLib>
    {
        BllControlMethodsLib Create(BllControlMethodsLib entity, bool isTemplate);
        new BllControlMethodsLib Update(BllControlMethodsLib entity, bool isTemplate);
        List<string> GetControlNamesById(int id);
        List<LiteControl> GetLiteControlsFromLib(int id);
    }
}
