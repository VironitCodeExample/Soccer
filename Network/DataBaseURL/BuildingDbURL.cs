using LazySoccer.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuildingDbURL", menuName = "Networking/BuildingDbURL", order = 2)]
public class BuildingDbURL : BaseDbURL<BuildingDbRequest>
{
    public BuildingDbRequest dictionatyURL;
}
[Serializable]
public class BuildingDbRequest : BaseDbRequest<BuildingRequests> { }
