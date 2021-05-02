namespace WMS.UserManagement.Model.Notifications
{
    interface IMessage
    {
        public string Content { get; set; }
        public void Send();
    }
}
