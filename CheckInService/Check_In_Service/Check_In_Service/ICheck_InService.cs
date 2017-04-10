using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;

namespace CheckInService
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface ICheck_InService
    {
        [OperationContract]
        void SetValue(LoginInfo val);

        [OperationContract]
        LoginInfo GetValue(int index);

        [OperationContract]
        bool LoginAccess(string email, string pass);

        [OperationContract]
        string sendMassage(string msg, string msg2);

        [OperationContract]
        object[] GetCoordinates(string login);

        [OperationContract]
        void AddNewCoordinates(double lat, double lon, string user);

        [OperationContract]
        bool SignUpUser(string log, string pas, string mail);
    }
}
