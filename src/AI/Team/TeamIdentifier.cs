using UnityEngine;

namespace U3.AI.Team
{
    public class TeamIdentifier : MonoBehaviour
    {
        [SerializeField] private TeamType m_TeamType;
        [SerializeField] private LayerMask layersToHit;
        [SerializeField] private LayerMask layersToDamage;

        public int LayersToHit => layersToHit;
        public int LayersToDamage => layersToDamage;

        private void Start()
        {
            // Register()
        }
    }
}
