using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatUIController : MonoBehaviour
    {
        public static CombatUIController Instance = null;

        public UnitSkillPanelControler UnitSkillPanelControler;
        public UnitRessourcesPanelController CurrentUnitRessourcesPanelController;
        public UnitRessourcesPanelController TargetUnitRessourcesPanelController;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(this);
        }
    }
}
