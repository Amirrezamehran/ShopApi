using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAggregate
{
    public class OrderAddress : BaseEntity
    {
        public OrderAddress(string province, string city, string postalCode, string postalAddress, string name, string family, string phoneNumber, string nationalCode)
        {
            Province = province;
            City = city;
            PostalCode = postalCode;
            PostalAddress = postalAddress;
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            NationalCode = nationalCode;
        }

        public long OrderId { get; internal set; }
        public Order Order { get; set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string NationalCode { get; private set; }
    }


}
