using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Exceptions
{
    public class NullOrEmptyDomainDataException : BaseDomainException
    {
        public NullOrEmptyDomainDataException(string message) : base(message)
        {

        }

        public NullOrEmptyDomainDataException()
        {

        }

        public static void CheckString(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrEmptyDomainDataException($"{fieldName} Is Null Or Empty!. . .");
            }
        }

    }


} // End Class
