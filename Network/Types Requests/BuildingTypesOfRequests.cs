using Cysharp.Threading.Tasks;
using LazySoccer.Network;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LazySoccer.Network.Get.GeneralClassGETRequest;

namespace LazySoccer.Network
{
    public class BuildingTypesOfRequests : BaseTypesOfRequests<BuildingRequests>
    {

        [SerializeField] private BuildingDbURL dbURL;
        public override string FullURL(string URL, BuildingRequests type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        public override async UniTask SendMessage(BuildingRequests type)
        {
            switch (type)
            {
                case BuildingRequests.AllBuilding:
                    await Get_AllBUildingRequast(type);
                    break;
                case BuildingRequests.UpdateBulding:
                    await Post_BuildingUpgrade(type);
                    break;
                case BuildingRequests.ImmediateUpdate:
                    await Post_ImmediateUpdate(type);
                    break;
                case BuildingRequests.Downgrade:
                    await Post_Downgrade(type);
                    break;
                default: break;
            }
        }
        [Button]
        private async UniTask Get_AllBUildingRequast(BuildingRequests type)
        {
            var result = await GetRequest(_networkingManager.BaseURL, type, Token: _managerPlayer.PlayerData.Token);
            Debug.Log(result.downloadHandler.text);
            var a = JsonConvert.DeserializeObject<List<BuildingAll>>(result.downloadHandler.text);

            ServiceLocator.GetService<ManagerBuilding>().RequestServer(a);
        }
        private string HouseUpdateDowngrade()
        {
            UpgradeBuilding upgrade = new UpgradeBuilding()
            {
                teamBuildingId = 0
            };
            return  JsonUtility.ToJson(upgrade);
        }

        [Button]
        private async UniTask Post_BuildingUpgrade(BuildingRequests type)
        {      
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: HouseUpdateDowngrade(), Token: _managerPlayer.PlayerData.Token, ActiveGlobalLoading: false);
        }

        private async UniTask Post_ImmediateUpdate(BuildingRequests type)
        {
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: HouseUpdateDowngrade(), Token: _managerPlayer.PlayerData.Token, ActiveGlobalLoading: false);
        }
        private async UniTask Post_Downgrade(BuildingRequests type)
        {
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: HouseUpdateDowngrade(), Token: _managerPlayer.PlayerData.Token, ActiveGlobalLoading: false);
        }
    }
}
