using LazySoccer.Network;
using System;
using UnityEngine;

public class BaseDbURL<T> : ScriptableObject 
{ 
    //public BaseDbRequest<T> dictionatyURL;
}
public class BaseDbRequest<T> : UnitySerializedDictionary<T, string> { }



