﻿namespace WMS.UserManagement.Model.Authentication
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
