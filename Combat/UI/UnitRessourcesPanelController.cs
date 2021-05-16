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
            _healthBar.currentPercent = (float)((float)unit.CurrentStats.HealthPoints / (float)unit.CombatEntity.UnitStats.HealthPoints) * 100;
            _manaBar.currentPercent = (float)((float)unit.CurrentStats.ManaPoints / (float)unit.CombatEntity.UnitStats.ManaPoints) * 100;

            int index = 0;
            int max = unit.Status.ActiveEffects.Count < MAX_STATUS_DISPLAY ? unit.Status.ActiveEffects.Count : MAX_STATUS_DISPLAY;
            UIStatus[] status = _statusBar.GetComponentsInChildren<UIStatus>(true);
            while (index < max)
            {
                status[index].UpdateState(ClassSkillLibrary.ClassEffectsDatas[unit.Status.ActiveEffects[index].Name].EffectDefinition);
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
