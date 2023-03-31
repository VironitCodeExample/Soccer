using Cysharp.Threading.Tasks;
using LazySoccer.Status;
using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Networking;
using static LazySoccer.Network.Error.ErrorRequest;
using static LazySoccer.Network.Post.AuthClassPostAnswer;
using static LazySoccer.Network.Post.AuthClassPostRequest;

namespace LazySoccer.Network
{
    public class AuthTypesOfRequests : BaseTypesOfRequests<AuthOfRequests>
    {
        [SerializeField] private AuthDbURL dbURL;
        public override string FullURL(string URL, AuthOfRequests type, string URLParam = "")
        {
            return URL + dbURL.dictionatyURL[type] + URLParam;
        }
        public override async UniTask SendMessage(AuthOfRequests type)
        {
            switch (type)
            {
                case AuthOfRequests.Login:
                    await Login(type);
                    break;
                case AuthOfRequests.MailConfirm:
                    await MailConfirm(type);
                    break;
                case AuthOfRequests.SendMailCode:
                    await SendMailCode(type);
                    break;
                case AuthOfRequests.RestorePassword:
                    await RestorePassword(type);
                    break;
                case AuthOfRequests.RefreshToken:
                    await RefreshToken(type);
                    break;
                    case AuthOfRequests.EnableTwoFactorAuth:
                    await EnableTwoFactorAuth(type);
                    break;
                case AuthOfRequests.TwoFactorAuth:
                    await TwoFactorAuth(type);
                    break;
                case AuthOfRequests.SignUp:
                    await SignUp(type);
                    break;
                default:break;
            }
        }
        protected async UniTask RequestOK(UnityWebRequest result, Action<UnityWebRequest> action)
        {
            switch (result.responseCode)
            {
                case 200:
                    Debug.Log("Hello");
                    action(result);
                    break;
                case 400:
                    var error400 = JsonUtility.FromJson<ServerError>(result.downloadHandler.text);
                    //_generalValidation.GetErrorMessentFromServer(DetailedError);
                    break;
                case 403:
                    var ValidationErrors = JsonConvert.DeserializeObject<ValidationErrors>(result.downloadHandler.text);
                    _generalValidation.GetErrorMessentFromServer(ValidationErrors);
                    break;
                case 404:
                    break;
                default:
                    Debug.LogError($"Error code: {result.responseCode}");
                    ServiceLocator.GetService<AuthenticationStatus>().SetAction(StatusAuthentication.Back);
                    ServiceLocator.GetService<LoadingStatus>().SetAction(StatusLoading.None);
                    break;
            }
        }

        #region Login
        private async UniTask Login(AuthOfRequests type)
        {
            var obj = new Login()
            {
                login = _managerPlayer.PlayerData.Email,
                password = _managerPlayer.PlayerData.Password
            };
            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData);
            await RequestOK(result, LoginResult);
        }
        private async void LoginResult(UnityWebRequest result)
        {
            var answer = JsonUtility.FromJson<LoginAnswer>(result.downloadHandler.text);
            _managerPlayer.PlayerData.Token = answer.token;
            if (answer.twoFactorEnabled)
            {
                _authentication.StatusAction = StatusAuthentication.TwoFA;
            }
            else
            {
                _managerLoading.ControlLoading(true);
                await LoadParam();
                //await _managerLoading.ActiveteLoading();
                _managerLoading.ControlLoading(false);

            }
        }
        private async UniTask LoadParam()
        {
            await _managerLoading.ActiveteLoading(ServiceLocator.GetService<GetUserTypesOfRequests>().SendMessage(GetUserOfRequests.GetUser), "Получаем информацию о Вас");
            if (_managerPlayer.PlayerHUDs.NameTeam.Value.Length == 0)
            {
                await _managerLoading.ActiveteLoading(_loadingScene.AssetLoaderScene("CreateTeam", StatusBackground.Active, new UniTask()));
                ServiceLocator.GetService<CreateTeamStatus>().StatusAction = StatusCreateTeam.CreateTeam;
            }
            else
            {
                await _managerLoading.ActiveteLoading(ServiceLocator.GetService<BuildingTypesOfRequests>().SendMessage(BuildingRequests.AllBuilding), "Строим Ваши здания");
                await _loadingScene.AssetLoaderScene("Game", StatusBackground.Active, new UniTask());
            }
        }
        #endregion

        #region MailConfirm
        private async UniTask MailConfirm(AuthOfRequests type)
        {
            var obj = new MailConfirm()
            {
                email = _managerPlayer.PlayerData.Email,
                code = _managerPlayer.PlayerData.Code
            };
            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData);
            await RequestOK(result, MailConfirmResult);
        }
        private async void MailConfirmResult(UnityWebRequest result)
        {
            await SendMessage(AuthOfRequests.Login);
        }
        #endregion

        #region SendMailCode
        private async UniTask SendMailCode(AuthOfRequests type)
        {
            var result = await PostRequest(_networkingManager.BaseURL, type, URLParam: _managerPlayer.PlayerData.Email);
            await RequestOK(result, SendMailCodeResult);
        }
        private void SendMailCodeResult(UnityWebRequest result)
        {
            _authentication.StatusAction = StatusAuthentication.RestorePasswordCode;
        }
        #endregion

        #region RestorePassword
        private async UniTask RestorePassword(AuthOfRequests type)
        {
            var obj = new RestorePassword()
            {
                email = _managerPlayer.PlayerData.Email,
                password = _managerPlayer.PlayerData.Password,
                code = _managerPlayer.PlayerData.Code
            };
            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData);
            await RequestOK(result, RestorePasswordResult);
        }
        private void RestorePasswordResult(UnityWebRequest result)
        {
            Login(AuthOfRequests.Login).Forget();
        }
        #endregion

        #region RefreshToken
        private async UniTask RefreshToken(AuthOfRequests type)
        {
            var token = _managerPlayer.PlayerData.Token;
            var result = await PostRequest(_networkingManager.BaseURL, type, Token: token);
        }
        private void RefreshTokenResult(UnityWebRequest result)
        {
            var answer = JsonUtility.FromJson<LoginAnswer>(result.downloadHandler.text);
            //_managerPlayer.PlayerData.Token = answer.token;
            if (answer.twoFactorEnabled)
            {
                _authentication.StatusAction = StatusAuthentication.TwoFA;
            }
            else
            {
                //_managerLoading.ActiveteLoading(_loadingScene.AssetLoaderScene("Game", StatusBackground.Active)).Forget();
            }
        }
        #endregion

        #region EnableTwoFactorAuth
        private async UniTask EnableTwoFactorAuth(AuthOfRequests type)
        {
            var token = _managerPlayer.PlayerData.Token;
            var result = await PostRequest(_networkingManager.BaseURL, type, Token: token);
            RequestOK(result, EnableTwoFactorAuthResult);
        }
        private void EnableTwoFactorAuthResult(UnityWebRequest result)
        {
            var answer = JsonUtility.FromJson<TwoFactorAuthentication>(result.downloadHandler.text);
            _managerPlayer.PlayerData.AuthenticationBarCodeImage = answer.authenticationBarCodeImage;
            _managerPlayer.PlayerData.AuthenticationManualCode = answer.authenticationManualCode;
        }
        #endregion

        #region TwoFactorAuth
        private async UniTask TwoFactorAuth(AuthOfRequests type)
        {
            var token = _managerPlayer.PlayerData.Token;
            var obj = new Login()
            {
                login = _managerPlayer.PlayerData.Email,
                password = _managerPlayer.PlayerData.Password
            };
            string jsonData = JsonUtility.ToJson(_managerPlayer.PlayerData.Pin);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData, Token: token);
            await RequestOK(result, TwoFactorAuthResult);
        }
        private async void TwoFactorAuthResult(UnityWebRequest result)
        {
            await _managerLoading.ActiveteLoading(_loadingScene.AssetLoaderScene("Game", StatusBackground.Active, LoadParam()));
        }
        #endregion

        #region SignUp
        private async UniTask SignUp(AuthOfRequests type)
        {
            var obj = new Register()
            {
                userName = _managerPlayer.PlayerData.UserName,
                email = _managerPlayer.PlayerData.Email,
                password = _managerPlayer.PlayerData.Password
            };
            string jsonData = JsonUtility.ToJson(obj);
            var result = await PostRequest(_networkingManager.BaseURL, type, JSON: jsonData);

            await RequestOK(result, SignUpResult);
        }
        private void SignUpResult(UnityWebRequest result)
        {
            _authentication.StatusAction = StatusAuthentication.EnterCode;
        }
        #endregion
    }
}
