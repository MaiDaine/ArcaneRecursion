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

        private CombatEntity _currentEntity = null;

        private readonly string[] _listObjectNames = { "Innate", "Class1", "Class2", "Class3", "End" };
        private const int _baseElemUISize = 65;
        private const int _maxSkillPerCat = 5;

        public void LoadCombatSkill(int index)
        {
            if (_currentEntity.GameObject.GetComponent<UnitController>().Skills.SelectCombatSkill(index / 5, index % 5))//TODO UI
                _onSkillSelected.Raise();
            else
                Debug.Log("SKILL IS ON CD");
        }

        public void UpdateAdvancePannel(int index)
        {
            List<CombatSkillObject> skills = _currentEntity.Build[index].Skills;

            ShowAdvancePannel();
            for (int i = 0; i < skills.Count; i++)
            {
                Transform elem = _advanceActionList.GetComponent<Transform>().Find(i.ToString());
                if (!skills[i].IsPassive)
                {
                    elem.gameObject.SetActive(true);
                    ButtonManagerBasic button = elem.GetComponent<ButtonManagerBasic>();
                    button.buttonText = skills[i].NodeDefinition.Name;
                    button.ClearListener();
                    button.AddIndexedListerner((index * _maxSkillPerCat) + i, LoadCombatSkill);
                    button.UpdateUI();
                    i++;
                }
                else
                    elem.gameObject.SetActive(false);


            }
            for (int i = skills.Count; i < _maxSkillPerCat; i++)
            {
                Transform elem = _advanceActionList.GetComponent<Transform>().Find(i.ToString());
                elem.gameObject.SetActive(false);
            }
        }

        public void SetUnitPannel(CombatEntity entity)
        {
            ButtonManagerBasic button;
            GameObject elem;
            int index = 0;

            _currentEntity = entity;
            foreach (ClassBuild e in entity.Build)
            {
                elem = _baseActionList.GetComponent<Transform>().Find(index.ToString()).gameObject;
                elem.SetActive(true);
                button = elem.GetComponent<ButtonManagerBasic>();
                button.ClearListener();
                button.AddIndexedListerner(index, UpdateAdvancePannel);
                button.buttonText = e.Name.ToString();
                button.UpdateUI();
                index++;
            }
            for (int i = index; i < _listObjectNames.Length - 1; i++)
                _baseActionList.GetComponent<Transform>().Find(i.ToString()).gameObject.SetActive(false);

            //RectTransform listTransform = listObject.GetComponent<RectTransform>();
            //int diff = 3 - entity.Build.Count;
            //listTransform.sizeDelta = new Vector2(listTransform.sizeDelta.x, 3 + ((ListObjectNames.Length - diff) * baseElemSize));
            //listTransform.position = new Vector3(listTransform.position.x, (ListObjectNames.Length - diff) * (baseElemSize / 2), listTransform.position.z);
        }

        public void ShowAdvancePannel() { _advanceActionGameObject.SetActive(true); }

        public void HideAdvancePannel() { _advanceActionGameObject.SetActive(false); }
    }
}