using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazySoccer.Network.Error
{
    public class ErrorRequest : MonoBehaviour
    {
        public class DetailedError
        {
            public string message;
            public string stackTrace;
            public object innerException;
            public List<object> innerExceptions;
        }

        public class ValidationErrors
        {
            public List<ValidationError> validationErrors;
            public string message;
            public DetailedError detailedError;
        }

        public class ValidationError
        {
            public string title;
            public object description;
            public string field;
        }

        public class Errors
        {
            public List<string> TeamName;
        }
        public class ServerError
        {
            public Errors errors;
            public string type;
            public string title;
            public int status;
            public string traceId;
        }
    }
}
