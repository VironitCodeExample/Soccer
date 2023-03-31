using Cysharp.Threading.Tasks;
using LazySoccer.Network;
using LazySoccer.Status;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static LazySoccer.Network.Post.TeamClassPostRequest;

namespace LazySoccer.Network
{
    public class CreateTeamTypesOfRequest : BaseTypesOfRequests<CreateTeamRequests>
    {
        [SerializeField] private CreateTeamDbURL dbURL;
        public override string FullURL(string URL, CreateTeamRequests type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        public override async UniTask SendMessage(CreateTeamRequests type)
        {
            switch (type)
            {
                case CreateTeamRequests.CreateTeam:
                    await PostCreateTeam(type);
                    break;
                case CreateTeamRequests.CreatePlayers:
                    await CreatePlayer(type);
                    break;
                case CreateTeamRequests.PlayerRoster:
                    await PlayerRoster(type);
                    break;
                default: break;
            }
        }
        protected bool RequestOK(UnityWebRequest result)
        {
            switch (result.responseCode)
            {
                case 200:
                    return true;
                case 400:
                case 403:
                case 404:
                case 500:
                default:
                    Debug.LogError($"Error code: {result.responseCode}");
                    ServiceLocator.GetService<CreateTeamStatus>().SetAction(StatusCreateTeam.CreateTeam);
                    return false;
            }
        }
        [Button]
        private async UniTask PostCreateTeam(CreateTeamRequests type)
        {
            Debug.Log("<color = red> PostCreateTeam </color>");
            var token = _managerPlayer.PlayerData.Token;
            var obj = new CreateTeamPost()
            {
                name = _managerPlayer.PlayerHUDs.NameTeam.Value,
                shortName = _managerPlayer.PlayerHUDs.NameShortTeam.Value
            };
            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData, Token: token, ActiveGlobalLoading: false);
            Debug.LogError(result.downloadHandler.text);
            if (RequestOK(result)) await CreateTeamResult();
        }
        private async UniTask CreateTeamResult()
        {
            await SendMessage(CreateTeamRequests.CreatePlayers);
        }

        private async UniTask CreatePlayer(CreateTeamRequests type)
        {
            Debug.Log("<color = red> CreatePlayer </color>");
            var token = _managerPlayer.PlayerData.Token;
            var result = await PostRequest(_networkingManager.BaseURL, type, Token: token, ActiveGlobalLoading: false);
            Debug.LogError(result.downloadHandler.text);
            if (RequestOK(result)) await CreatePlayerResult();
        }
        private async UniTask CreatePlayerResult()
        {
            await SendMessage(CreateTeamRequests.PlayerRoster);
        }

        private async UniTask PlayerRoster(CreateTeamRequests type)
        {
            Debug.Log("<color = red> PlayerRoster </color>");
            var token = _managerPlayer.PlayerData.Token;
            var result = await GetRequest(_networkingManager.BaseURL, type, Token: token);
            Debug.LogError(result.downloadHandler.text);
            if (RequestOK(result)) await PlayerRosterResult();
        }
        private async UniTask PlayerRosterResult()
        {
            ServiceLocator.GetService<CreateTeamStatus>().SetAction(StatusCreateTeam.StartingPlayers);
        }
    }
}
