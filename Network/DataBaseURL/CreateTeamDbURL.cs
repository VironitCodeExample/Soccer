using LazySoccer.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateTeamDbURL", menuName = "Networking/CreateTeamDbURL", order = 2)]
public class CreateTeamDbURL : BaseDbURL<CreateTeamRequests>
{
    public CreateTeamDbRequest dictionatyURL;
}
[Serializable]
public class CreateTeamDbRequest : BaseDbRequest<CreateTeamRequests> { }
