using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using CheckInEF;
using System.Collections;

namespace CheckInService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Check_InService : ICheck_InService
    {
        public Check_InService()
        {
            Connection();
        }
        public static List<LoginInfo> LoginList = new List<LoginInfo>();

        public void SetValue(LoginInfo user)
        {
            LoginList.Add(user);
        }

        public LoginInfo GetValue(int index)
        {
            Connection();
            return LoginList[index];
        }

        public string sendMassage(string msg, string msg2)
        {
            string str = "";
            return str;
        }

        /*Adding new user to database*/
        public bool SignUpUser(string log, string pas, string mail)
        {
            using (var context = new CheckInEntities())
            {
                try
                {
                    context.LoginInfoes.Add(new CheckInEF.LoginInfo() { Login = log, Password = pas, Email = mail });
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /*Checking if user is in database*/
        public bool LoginAccess(string email, string pass)
        {
            foreach (var info in LoginList)
            {
                if (info.Email.Equals(email) && info.Password.Equals(pass))
                {
                    return true;
                }
            }
            return false;
        }

        private class TripleL
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Login { get; set; }
        }

        public object[] GetCoordinates(string login)
        {
            using (var context = new CheckInEntities())
            {
                var coords = from coord in context.Coordinates
                             join log in context.LoginInfoes
                             on coord.UserFK equals log.Email
                             where log.Email.Equals(login)
                             select new TripleL()
                             {
                                       Latitude = coord.Latitude,
                                       Longitude = coord.Longitude,
                                       Login = coord.UserFK
                             };
                return coords.ToArray<TripleL>();
            }
        }


        /*Establish connection with database*/
        private void Connection()
        {
            using (var context = new CheckInEntities())
            {
                foreach (var log in context.LoginInfoes)
                {
                    SetValue(new LoginInfo { Login = log.Login, Password = log.Password, Email = log.Email });
                }
            }
        }

        public void AddNewCoordinates(double lat, double lon, string user)
        {
            using (var context = new CheckInEntities())
            {
                context.Coordinates.Add(new Coordinates { Latitude = lat, Longitude = lon, UserFK = user});
                context.SaveChanges();
            }
        }
    }
}
