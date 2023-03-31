using Cysharp.Threading.Tasks;
using LazySoccer.Status;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace LazySoccer.Network
{
    public class ChangeUserTypesOfRequests : BaseTypesOfRequests<ChangeOfRequests>
    {
        [SerializeField] private ChangeUserDbURL dbURL;
        public override string FullURL(string URL, ChangeOfRequests type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        public override async UniTask SendMessage(ChangeOfRequests type)
        {
            switch (type)
            {
                case ChangeOfRequests.ChangePassword:
                    await ChangePassword(type);
                    break;
                case ChangeOfRequests.ChangeNickName:
                    await ChangeName(type);
                    break;
                case ChangeOfRequests.ChangeMail:
                    //await ChangePassword(type);
                    break;
                case ChangeOfRequests.SendCodeToNewMail:
                    //await ChangePassword(type);
                    break;
                default: break;
            }
        }
        protected void RequestOK(UnityWebRequest result, Action<UnityWebRequest> action)
        {
            switch (result.responseCode)
            {
                case 200:
                    action(result);
                    break;
                default:
                    Debug.LogError($"Error code: {result.responseCode}");
                    break;
            }
        }
        #region ChangePassword
        private async UniTask ChangePassword(ChangeOfRequests type)
        {
            var obj = new ChangePassword()
            {
                password = _managerPlayer.PlayerData.Password,
                newPassword = _managerPlayer.PlayerData.PasswordConfirn
            };

            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData, Token: _managerPlayer.PlayerData.Token);
            RequestOK(result, ResultStatusClose);
        }
        private async UniTask ChangeName(ChangeOfRequests type)
        {
            var obj = new ChangeName()
            {
                userName = _managerPlayer.PlayerData.UserName,
            };

            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData, Token: _managerPlayer.PlayerData.Token);
            RequestOK(result, ResultStatusClose);
        }
        #endregion
        private void ResultStatusClose(UnityWebRequest result)
        {
            ServiceLocator.GetService<ModalWindowStatus>().SetAction(StatusModalWindows.None);
        }
    }
}
