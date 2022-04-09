using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}
