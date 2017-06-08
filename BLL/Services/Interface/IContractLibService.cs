using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IContractLibService : IService<BllContractLib>
    {
        new BllContractLib Create(BllContractLib entity);
        new BllContractLib Update(BllContractLib entity);
    }
}
