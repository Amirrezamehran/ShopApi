using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAggregate.Enums;
using Shop.Domain.UserAggregate.Services;
using System;


namespace Shop.Domain.UserAggregate
{
    public class User : AggregateRoot
    {
        private User()
        {

        }

        public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
            AvatarName = "avatar.png";
            IsActive = true;
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string AvatarName { get; private set; }
        public bool IsActive { get; private set; }
        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; private set; }
        public List<UserAddress> Addresses { get; private set; }
        public List<Wallet> Wallets { get; private set; }



        public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
        {
            return new User("", "", phoneNumber, password, null, Gender.None, userDomainService);
        }

        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                AvatarName = "avatar.png";
            }

            AvatarName = imageName;
        }

        public void EditUser(string name, string family, string phoneNumber, string email, Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }

        public void EditAddress(UserAddress address, long addressId)
        {
            // از نوع رفرنس تایپ است و وقتی تغییرش بدیم یا ادیتش کنیم در همه جا تغییر میکند oldAddress
            // و در زیر هم ما اومدیم ادیتش کردیم خودش میره همه جا تغییرش میده
            var oldAddress = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (oldAddress == null)
            {
                throw new NullOrEmptyDomainDataException("Address Not Found!");
            }

            oldAddress.EditAddress(address.Province, address.City, address.PostalCode, address.PostalAddress, address.Name
                            , address.Family, address.PhoneNumber, address.NationalCode);
        }

        public void DeleteAddress(long address)
        {
            var oldAddress = Addresses.FirstOrDefault(a => a.Id == address);
            if (oldAddress == null)
            {
                throw new NullOrEmptyDomainDataException("Address Not Found!");
            }

            Addresses.Remove(oldAddress);
        }

        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }

        public void AddRoles(List<UserRole> roles)
        {
            roles.ForEach(p => p.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }

        private void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            // تو این پروژه اینو نمیخوام چون ثبت نام با ایمیل نداریم در این پروژه
            //NullOrEmptyDomainDataException.CheckString(email, nameof(email));

            if (phoneNumber.Length != 11)
            {
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");
            }

            if (!string.IsNullOrWhiteSpace(email))
                if (email.IsValidEmail() == false)
                {
                    throw new InvalidDomainDataException("ایمیل نامعتبر است");
                }

            if (phoneNumber != PhoneNumber)
            {
                if (userDomainService.IsPhoneNumberExist(phoneNumber))
                {
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");
                }
            }

            if (email != Email)
            {
                if (userDomainService.IsEmailExist(email))
                {
                    throw new InvalidDomainDataException("ایمیل تکراری است");
                }
            }


        }




    }


} // End Class
