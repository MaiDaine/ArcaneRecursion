using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatLibrary : MonoBehaviour
    {
        public static CombatLibrary Instance;
        public Material[] TileMaterials;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}