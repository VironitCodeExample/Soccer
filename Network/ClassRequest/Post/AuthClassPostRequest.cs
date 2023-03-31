using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazySoccer.Network.Post
{
    public class AuthClassPostRequest
    {
        #region Login
        public class Login
        {
            public string login;
            public string password;
        }
        #endregion

        #region MailConfirm
        public class MailConfirm
        {
            public string email;
            public string code;
        }
        #endregion

        #region RestorePassword
        public class RestorePassword
        {
            public string email;
            public string password;
            public string code;
        }
        #endregion

        #region Register
        public class Register
        {
            public string userName;
            public string email;
            public string password;
        }
        #endregion
    }
}
