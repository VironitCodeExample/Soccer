using LazySoccer.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetUserDbURL", menuName = "Networking/GetUserDbURL", order = 2)]
public class GetUserDbURL : BaseDbURL<GetUserOfRequests>
{
    public GetUserDbRequest dictionatyURL;
}
[Serializable]
public class GetUserDbRequest : BaseDbRequest<GetUserOfRequests> { }
