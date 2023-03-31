using LazySoccer.Network;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AuthDbURL", menuName = "Networking/AuthDbURL", order = 1)]
public class AuthDbURL : BaseDbURL<AuthDbRequest>
{
    public AuthDbRequest dictionatyURL;
}
[Serializable]
public class AuthDbRequest : BaseDbRequest<AuthOfRequests> { }
