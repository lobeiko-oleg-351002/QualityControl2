using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllRequirementDocumentationLib : IBllEntityLib<BllRequirementDocumentation>
    {
        public int Id { get; set; }

        public List<BllSelectedEntity<BllRequirementDocumentation>> SelectedEntities { get; set; }

        public BllRequirementDocumentationLib()
        {
            SelectedEntities = new List<BllSelectedEntity<BllRequirementDocumentation>>();
        }
    }
}
