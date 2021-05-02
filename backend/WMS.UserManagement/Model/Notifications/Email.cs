using System;
using WMS.UserManagement.Model.Notifications;

namespace WMS.UserManagement.Model.Db
{
    public class Email : IMessage
    {
        public string EmailAddress { get; set; }
        public string Content { get; set; }

        public void Send()
        {
            throw new NotImplementedException();
        }
    }
}
