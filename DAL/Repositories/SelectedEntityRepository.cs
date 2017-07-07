using DAL.Entities;
using DAL.Entities.Interface;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SelectedEntityRepository<UEntity> : Repository<IDalSelectedEntity, UEntity, SelectedEntityMapper<UEntity>>, ISelectedEntityRepository<UEntity>, IGetterByLibId<IDalSelectedEntity>
        where UEntity : class, ISelectedEntity, new()
    {
        private readonly ServiceDB context;
        public SelectedEntityRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<IDalSelectedEntity> GetEntitiesByLibId(int id)
        {
            var elements = context.Set<UEntity>().Where(entity => entity.lib_id == id);
            var retElemets = new List<IDalSelectedEntity>();
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

    }
}
