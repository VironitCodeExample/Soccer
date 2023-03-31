using Cysharp.Threading.Tasks;
using LazySoccer.Network;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LazySoccer.Network.Get.GeneralClassGETRequest;

namespace LazySoccer.Network
{
    public class GetUserTypesOfRequests : BaseTypesOfRequests<GetUserOfRequests>
    {
        [SerializeField] private GetUserDbURL dbURL;
        public override string FullURL(string URL, GetUserOfRequests type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        public override async UniTask SendMessage(GetUserOfRequests type)
        {
            switch (type)
            {
                case GetUserOfRequests.GetUser:
                    await GetUserRequast(type);
                    break;
                case GetUserOfRequests.TeamRoster:

                    break;
                case GetUserOfRequests.TeamRosterId:
                    //await ChangePassword(type);
                    break;
                default: break;
            }
        }
        [Button]
        private async UniTask GetUserRequast(GetUserOfRequests type)
        {
            var result = await GetRequest(_networkingManager.BaseURL, type, Token: _managerPlayer.PlayerData.Token);
            var a = CreateFromJSON<GetUser>(result.downloadHandler.text);
            _managerPlayer.PlayerHUDs.AddParam(a);
        }
        public static T CreateFromJSON<T>(string jsonString)
        {
            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}
