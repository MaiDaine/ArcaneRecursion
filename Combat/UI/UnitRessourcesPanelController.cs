using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitRessourcesPanelController : MonoBehaviour
    {
        [SerializeField] private ProgressBar _healthBar;
        [SerializeField] private ProgressBar _manaBar;
        [SerializeField] private GameObject _statusBar;

        private const int MAX_STATUS_DISPLAY = 16;

        public void ClearPannel() { gameObject.SetActive(false); }

        public void SetTargetUnit(UnitController unit)
        {
            gameObject.SetActive(true);
            _healthBar.currentPercent = (float)((float)unit.CurrentStats.HealthPoint / (float)unit.CombatEntity.UnitStats.HealthPoint) * 100;
            _manaBar.currentPercent = (float)((float)unit.CurrentStats.ManaPoint / (float)unit.CombatEntity.UnitStats.ManaPoint) * 100;

            int index = 0;
            int max = unit.Status.ActiveEffects.Count < MAX_STATUS_DISPLAY ? unit.Status.ActiveEffects.Count : MAX_STATUS_DISPLAY;
            UIStatus[] status = _statusBar.GetComponentsInChildren<UIStatus>(true);
            while (index < max)
            {
                status[index].UpdateState(CombatLibrary.Instance.StatusLibrary.GetEffectDefinition(unit.Status.ActiveEffects[index].Name));
                status[index].gameObject.SetActive(true);
                index++;
            }
            while (index < MAX_STATUS_DISPLAY)
            {
                status[index].gameObject.SetActive(false);
                index++;
            }
        }
    }
}
