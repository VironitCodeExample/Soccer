using LazySoccer.Network;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeUserDbURL", menuName = "Networking/ChangeUserDbURL", order = 2)]
public class ChangeUserDbURL : BaseDbURL<ChangeDbRequest>
{
    public ChangeDbRequest dictionatyURL;
}
[Serializable]
public class ChangeDbRequest : BaseDbRequest<ChangeOfRequests> { }