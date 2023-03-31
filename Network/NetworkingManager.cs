using Cysharp.Threading.Tasks;
using LazySoccer.Status;
using System;
using UnityEngine;
using LazySoccer.SceneLoading;

namespace LazySoccer.Network
{
    public class NetworkingManager : MonoBehaviour
    {
        [SerializeField] private string LocalBaseURL = @"https://localhost:7199/api/v1/";
        [SerializeField] private string GlobalBaseURL = @"http://192.168.1.33:2525/api/v1/";

#if UNITY_EDITOR
        [SerializeField] private bool isLocalURL = true;
#endif
        public string BaseURL
        {
            get
            {
#if UNITY_EDITOR
                if (isLocalURL)
                    return LocalBaseURL;
                else
                    return GlobalBaseURL;
#else
            return GlobalBaseURL;
#endif
            }
            private set
            {
#if UNITY_EDITOR
                LocalBaseURL = value;
#else
            GlobalBaseURL = value;
#endif
            }
        }
    }
}
