using TMPro;
using Michsky.UI.ModernUIPack;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace ArcaneRecursion
{
    public class TalentTreeContoller : MonoBehaviour
    {
        public GameObject TalentButtonPrefab;
        public TalentUIController MainController;
        public GameObject TreeWrapper;
        public int TreeIndex { get; set; }

        private void Awake()
        {
            CustomDropdown dropDown = GetComponentInChildren<CustomDropdown>();
            dropDown.dropdownEvent.AddListener(OnDropdownSelection);
        }

        private void LoadNodeInfo(Transform node, NodeDefinition data)
        {
            node.GetComponent<Image>().sprite = data.Icon;
            node.GetComponent<TooltipContent>().description = data.Name + "\n\n" + data.Description;
        }

        private void LoadNodeButtonInfo(Transform node, NodeDefinition data)
        {
            node.GetComponent<ButtonManagerIcon>().buttonIcon = data.Icon;
            node.GetComponent<TooltipContent>().description = data.Name + "\n\n" + data.Description;
        }

        private void LoadTierInfo(Transform talentTier, NodeDefinition[] datas)
        {
            foreach (NodeDefinition node in datas)
            {
                GameObject button = Instantiate(TalentButtonPrefab);
                LoadNodeButtonInfo(button.transform, node);
                button.transform.SetParent(talentTier, false);
            }
        }

        public void OnDropdownSelection(int select) { MainController.OnDropdownSelection(TreeIndex, select); }

        public void LoadClassDefinitionToUI(ClassDefinition classDefinition)
        {
            TreeWrapper.transform.Find("Class_Text").GetComponent<TextMeshProUGUI>().text = classDefinition.Name;

            Transform spellBlock = TreeWrapper.transform.Find("Spell_Block");
            LoadNodeInfo(spellBlock.Find("Passive"), classDefinition.Spells.Passive);
            LoadNodeInfo(spellBlock.Find("Spell_1"), classDefinition.Spells.Spell1);
            LoadNodeInfo(spellBlock.Find("Spell_2"), classDefinition.Spells.Spell2);
            LoadNodeInfo(spellBlock.Find("Spell_3"), classDefinition.Spells.Spell3);
            LoadNodeInfo(spellBlock.Find("Spell_4"), classDefinition.Spells.Spell4);

            Transform talentBlock = TreeWrapper.transform.Find("Talent_Block");
            GameObject ultimate = Instantiate(TalentButtonPrefab);
            ultimate.transform.SetParent(talentBlock.Find("Tier_5"));
            ultimate.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            LoadNodeButtonInfo(ultimate.transform, classDefinition.Spells.Ultimate);
            LoadTierInfo(talentBlock.Find("Tier_1"), classDefinition.Talents.Tier1);
            LoadTierInfo(talentBlock.Find("Tier_2"), classDefinition.Talents.Tier2);
            LoadTierInfo(talentBlock.Find("Tier_3"), classDefinition.Talents.Tier3);
            LoadTierInfo(talentBlock.Find("Tier_4"), classDefinition.Talents.Tier4);

            TreeWrapper.SetActive(true);
            GetComponentInChildren<CustomDropdown>().gameObject.SetActive(false);
        }

        public void RefreshDropDownInfos(List<CustomDropdown.Item> availableClasses)
        {
            CustomDropdown dropDown = GetComponentInChildren<CustomDropdown>();
            CustomDropdown.Item emptyItem = new CustomDropdown.Item();

            emptyItem.itemName = "";
            emptyItem.itemIcon = null;

            dropDown.dropdownItems.Clear();
            dropDown.dropdownItems.Add(emptyItem);
            dropDown.dropdownItems.AddRange(availableClasses);
            dropDown.SetupDropdown();
        }
    }
}