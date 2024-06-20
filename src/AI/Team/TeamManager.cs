using System;
using System.Collections.Generic;
using UnityEngine;

namespace U3.AI.Team
{
    public static class TeamManager
    {
        private readonly static Dictionary<TeamType, int> teamInstanceIDs = new();

        public static int GetTeamInstanceID(TeamType teamType)
        {
            return teamInstanceIDs[teamType];
        }

        private static void SetTeamInstanceIDs()
        {
            foreach (TeamType teamType in Enum.GetValues(typeof(TeamType)))
            {
                GameObject teamInstancePlaceholder = new(teamType.ToString());
                teamInstanceIDs[teamType] = teamInstancePlaceholder.GetInstanceID();
                GameObject.Destroy(teamInstancePlaceholder);
            }
        }

        public static void Init()
        {
            SetTeamInstanceIDs();
        }
    }
}