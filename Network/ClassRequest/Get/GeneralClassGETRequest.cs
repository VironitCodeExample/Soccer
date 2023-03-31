using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LazySoccer.Network.Get
{
    public class GeneralClassGETRequest
    {
        #region Request GetUser
        public class Role
        {
            public string roleName;
        }

        public class GetUser
        {
            public string email;
            public string userName;
            public string country;
            public string teamName;
            public int balance;
            public int matchCount;
            public int winsCount;
            public bool firstLogin;
            public bool twoFactorEnabled;
            public bool lockoutEnabled;
            public bool emailConfirmed;
            public List<Role> roles;

            public override string ToString()
            {
                return $"{email}  {userName}";
            }
        }

        #endregion

        #region Building
        public class BuildingAll
        {
            public int id;
            public DateTime? dateOfCompletion;
            public DateTime? dateOfStart;
            public string buildingType;
            public int level;
            public string description;
            public int buildingLvLId;
            public int maintenanceCost;
            public int buildingCost;
            public int costInstantBuilding;
            public double buildTime;
        }
        public class UpgradeBuilding
        {
            public int teamBuildingId;
        }
        #endregion
    }
}
