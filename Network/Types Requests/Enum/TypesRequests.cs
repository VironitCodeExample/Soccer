namespace LazySoccer.Network
{
    public enum TypesRequests
    {
        Auth,
        Change,
    }

    public enum AuthOfRequests
    {
        None,
        Login,
        MailConfirm,
        SendMailCode,
        RestorePassword,
        EnableTwoFactorAuth,
        TwoFactorAuth,
        RefreshToken,
        SignUp,
    }
    public enum ChangeOfRequests
    {
        None,
        ChangePassword,
        ChangeNickName,
        ChangeMail,
        SendCodeToNewMail,
    }
    public enum GetUserOfRequests 
    {
        None,
        GetUser,
        TeamRoster,
        TeamRosterId,
    }

    public enum BuildingRequests
    {
        None,
        AllBuilding,
        UpdateBulding,
        ImmediateUpdate,
        Downgrade
    }
    public enum CreateTeamRequests
    {
        None,
        CreateTeam,
        CreatePlayers,
        PlayerRoster
    }

}