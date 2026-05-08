using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAggregate.Services
{
    public interface ISellerDomainService
    {
        bool CheckSellerInfo(Seller seller);
        bool NationalCodeExistInDatabase(string nationalCode);
    }
}
