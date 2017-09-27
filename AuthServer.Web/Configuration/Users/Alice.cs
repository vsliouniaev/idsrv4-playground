using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace AuthServer.Web.Configuration.Users
{
    public class Alice : TestUser
    {
        public Alice()
        {
            SubjectId = "1";
            Username = "alice";
            Password = "pass";
        }
    }
}
