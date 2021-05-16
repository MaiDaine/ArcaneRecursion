using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitSkillPanelControler : MonoBehaviour
    {
        [SerializeField] private GameEvent _onSkillSelected;
        [SerializeField] private GameObject _baseActionGameObject;
        [SerializeField] private GameObject _baseActionList;
        [SerializeField] private GameObject _advanceActionGameObject;
        [SerializeField] private GameObject _advanceActionList;

        private UnitController _currentUnit = null;

        private readonly string[] _listObjectNames = { "Innate", "Class1", "Class2", "Class3", "End" };
        private const int _baseElemUISize = 65;
        private const int _maxSkillPerCat = 5;

        public void LoadCombatSkill(int index)
        {
            if (_currentUnit.Skills.SelectCombatSkill(index / 5, index % 5))//TODO UI
                _onSkillSelected.Raise();
            else
                Debug.Log("SKILL IS ON CD");
        }

        public void UpdateAdvancePannel(int index)
        {
            List<UnitSkill> skills = _currentUnit.Skills.AvailableSkills[index];
            ShowAdvancePannel();
            for (int i = 0; i < skills.Count; i++)
            {
                Transform elem = _advanceActionList.GetComponent<Transform>().Find(i.ToString());
                elem.gameObject.SetActive(true);
                ButtonManagerBasic button = elem.GetComponent<ButtonManagerBasic>();
                button.buttonText = skills[i].SkillData.SkillDefinition.Name;
                button.ClearListener();
                button.AddIndexedListerner((index * _maxSkillPerCat) + i, LoadCombatSkill);
                if (skills[i].Cooldown != 0)
                    elem.GetComponent<UnityEngine.UI.Button>().interactable = false;
                button.UpdateUI();
            }
            for (int i = skills.Count; i < _maxSkillPerCat; i++)
            {
                Transform elem = _advanceActionList.GetComponent<Transform>().Find(i.ToString());
                elem.gameObject.SetActive(false);
            }
        }

        public void SetUnitPannel(UnitController unit)
        {
            ButtonManagerBasic button;
            GameObject elem;
            int index = 0;

            _currentUnit = unit;
            while (index < unit.Skills.AvailableSkills.Length && unit.Skills.AvailableSkills[index] != null)
            {
                elem = _baseActionList.GetComponent<Transform>().Find(index.ToString()).gameObject;
                elem.SetActive(true);
                button = elem.GetComponent<ButtonManagerBasic>();
                button.ClearListener();
                button.AddIndexedListerner(index, UpdateAdvancePannel);
                button.buttonText = unit.Skills.BuildClassNames[index].ToString();
                button.UpdateUI();
                index++;
            }
            for (int i = index; i < _listObjectNames.Length - 1; i++)
                _baseActionList.GetComponent<Transform>().Find(i.ToString()).gameObject.SetActive(false);
            HideAdvancePannel();
        }

        public void ShowAdvancePannel() { _advanceActionGameObject.SetActive(true); }

        public void HideAdvancePannel() { _advanceActionGameObject.SetActive(false); }
    }
}