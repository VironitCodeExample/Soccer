
using System.Collections.Generic;

namespace LazySoccer.Network.Post
{
    public class AuthClassPostAnswer
    {
        #region Login Answer
        public class LoginAnswer
        {
            public string token;
            public string refreshToken;
            public bool twoFactorEnabled;
        }
        #endregion
    }
}
