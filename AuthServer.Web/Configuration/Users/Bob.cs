using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace AuthServer.Web.Configuration.Users
{
    public class Bob : TestUser
    {
        public Bob()
        {
            SubjectId = "2";
            Username = "bob";
            Password = "pass";
        }
    }
}
