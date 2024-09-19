using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace BanditDynamite.Components
{
    public class FixGhostDesync: MonoBehaviour
    {
        public void OnDisable()
        {
            // Main.Log.LogInfo("removing ghost lol");
            Destroy(gameObject);
        }
    }
}
