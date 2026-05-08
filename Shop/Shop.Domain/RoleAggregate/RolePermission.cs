using Common.Domain;
using Shop.Domain.RoleAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.RoleAggregate
{
    public class RolePermission : BaseEntity
    {
        public long RoleId { get; internal set; }
        public Permission Permissions { get; private set; }

        public RolePermission(Permission permissions)
        {
            Permissions = permissions;
        }
    }

}
