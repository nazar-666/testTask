using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Place.Services.MessageModels
{
    [DataContract]
    public class EmailConfirmation //use for ResetPassword Email
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CallbackUrl { get; set; }

        public EmailConfirmation()
        {
        }

        public EmailConfirmation(string userName, string callbackUrl)
        {
            UserName = userName;
            CallbackUrl = callbackUrl;
        }
    }

    public class EmailConfirmationRegistration : EmailConfirmation
    {
        [DataMember]
        public string Password { get; set; }
        public EmailConfirmationRegistration()
        {
        }

        public EmailConfirmationRegistration(string userName, string callbackUrl, string password)
        {
            UserName = userName;
            CallbackUrl = callbackUrl;
            Password = password;
        }
    }
}
