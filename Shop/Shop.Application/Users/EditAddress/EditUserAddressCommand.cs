using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.EditAddress
{
    public class EditUserAddressCommand : IBaseCommand
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public string NationalCode { get; private set; }

        public EditUserAddressCommand(long id, long userId, string province, string city, string postalCode, string postalAddress,
            string name, string family, PhoneNumber phoneNumber, string nationalCode)
        {
            Id = id;
            UserId = userId;
            Province = province;
            City = city;
            PostalCode = postalCode;
            PostalAddress = postalAddress;
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            NationalCode = nationalCode;
        }
    }
}
