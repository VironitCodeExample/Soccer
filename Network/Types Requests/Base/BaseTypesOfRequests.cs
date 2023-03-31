using Cysharp.Threading.Tasks;
using LazySoccer.SceneLoading;
using LazySoccer.Status;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace LazySoccer.Network
{
    public class BaseTypesOfRequests<T> : MonoBehaviour
    {
        protected NetworkingManager _networkingManager;
        protected GeneralValidation _generalValidation;
        protected AuthenticationStatus _authentication;
        protected ManagerPlayerData _managerPlayer;
        protected ManagerLoading _managerLoading;
        protected LoadingScene _loadingScene;

        //[SerializeField] protected virtual BaseDbURL<T> dbURL;

        public virtual async UniTask SendMessage(T type) { }

        protected virtual void ResultRequest(int code) { }

        protected virtual void Start()
        {
            _authentication = ServiceLocator.GetService<AuthenticationStatus>();
            _generalValidation = ServiceLocator.GetService<GeneralValidation>();
            _managerPlayer = ServiceLocator.GetService<ManagerPlayerData>();
            _managerLoading = ServiceLocator.GetService<ManagerLoading>();
            _loadingScene = ServiceLocator.GetService<LoadingScene>();
            _networkingManager = ServiceLocator.GetService<NetworkingManager>();
        }

        public virtual async UniTask<UnityWebRequest> PostRequest(string URL, T type, string URLParam = "", string JSON = "", string Token = "", bool ActiveGlobalLoading = true)
        {
            if(ActiveGlobalLoading) _managerLoading.ControlLoading(true);
            var uwr = new UnityWebRequest(FullURL(URL, type, URLParam), "POST");

            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            if (JSON != null)
                SendOneJSON(JSON, ref uwr);
            if (JSON != null)
                SendOneToken(Token, ref uwr);

            await uwr.Send();

            if (uwr.error != null)
            {
                Debug.LogError($"Error {uwr.error}");
                return uwr;
            }
            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Error Connection {uwr.error}");
                return null;
            }
            if (ActiveGlobalLoading) _managerLoading.ControlLoading(false);
            return uwr;

        }
        public virtual async UniTask<UnityWebRequest> GetRequest(string URL, T type, string URLParam = "", string Token = "")
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(FullURL(URL, type, URLParam));

            webRequest.SetRequestHeader("Authorization", $"Bearer {Token}");
            await webRequest.SendWebRequest();

            string[] pages = URL.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    return null;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    return null;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    return webRequest;
            }
            return webRequest;
        }
        public virtual string FullURL(string URL, T type, string URLParam = "") => URL + URLParam;
        private void SendOneJSON(string JSON, ref UnityWebRequest request)
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(JSON);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.SetRequestHeader("Content-Type", "application/json");
        }
        private void SendOneToken(string Token, ref UnityWebRequest request)
        {
            request.SetRequestHeader("Authorization", $"Bearer {Token}");
        }
    }
}
