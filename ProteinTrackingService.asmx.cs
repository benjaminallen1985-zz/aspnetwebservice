using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ProteinTrackerWebService
{
    [WebService(Namespace = "http://Awebsite.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ProteinTrackingService : WebService
    {

        public class AuthenticationHeader : SoapHeader
        {
            public string UserName;
            public string Password;
        }

        public AuthenticationHeader Authentication;

        private UserRepository repository = new UserRepository();

        [WebMethod(Description = "Adds an amount to the total.", EnableSession = true)]
        [SoapHeader("Authentication")]
        public int AddProtein(int amount, int userId)
        {
            if (Authentication == null || Authentication.UserName != "Dude" || Authentication.Password != "Pass")
                throw new Exception("Crap");
            var user = repository.GetById(userId);
            if (user == null)
                return -1;
            user.Total += amount;
            repository.save(user);
            return user.Total;

            //var total = 0;
            //if (Session["user" + userId] == null)
            //    return -1;
            //var user = (User)Session["user" + userId];
            //user.Total += amount;
            //Session["user" + userId] = user;
            //return user.Total;
        }

        [WebMethod(EnableSession = true)]
        public int AddUser(string name, int goal)
        {
            var user = new User { Goal = goal, Name = name, Total = 0 };
            repository.Add(user);

            return user.UserID;

            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["userId"];
            //Session["user" + userId] = new User { Goal = goal, Name = name, Total = 0, UserID = userId };
            //Session["userId"] = userId + 1;
            //return userId;
        }

        [WebMethod(EnableSession = true)]
        public List<User> ListUsers()
        {
            return new List<User>(repository.GetAll());

            //var user = new List<User>();
            //var userId = 0;
            //if (Session["userId"] != null)
            //    userId = (int)Session["userId"];
            //for(var i = 0; i < userId; i++)
            //{
            //    user.Add((User) Session["user" + i]);
            //}

            //return user;
        }
               
    }
}
