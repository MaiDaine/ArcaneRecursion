using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace ArcaneRecursion
{
    public class TalentUIController : MonoBehaviour
    {
        public ClassDefinition[] ClassDefinitions;
        public TalentTreeContoller[] SubTrees;

        private readonly ClassDefinition[] _selection = new ClassDefinition[3];
        private readonly List<CustomDropdown.Item> _availableClasses = new List<CustomDropdown.Item>();

        private void Awake()
        {
            for (int i = 0; i < _selection.Length; i++)
                _selection[i] = null;
        }

        private void Start()
        {
            RefreshAvailableClasses();

            for (int i = 0; i < _selection.Length; i++)
            {
                if (_selection[i] == null)
                {
                    SubTrees[i].TreeIndex = i;
                }
                SubTrees[i].RefreshDropDownInfos(_availableClasses);
            }
        }

        private void RefreshAvailableClasses()
        {
            _availableClasses.Clear();

            for (int i = 0; i < ClassDefinitions.Length; i++)
            {
                bool inUse = false;
                for (int j = 0; j < _selection.Length; j++)
                {
                    if (_selection[j] != null && ClassDefinitions[i] == _selection[j])
                        inUse = true;
                }

                if (!inUse)
                {
                    CustomDropdown.Item item = new CustomDropdown.Item();
                    item.itemName = ClassDefinitions[i].Name;
                    item.itemIcon = ClassDefinitions[i].Spells.Passive.Icon;
                    _availableClasses.Add(item);
                }
            }
        }

        private void RefreshDropdownAvailableSelection()
        {
            RefreshAvailableClasses();

            for (int i = 0; i < _selection.Length; i++)
            {
                if (_selection[i] == null)
                    SubTrees[i].RefreshDropDownInfos(_availableClasses);
            }
        }

        public void OnDropdownSelection(int treeIndex, int select)
        {
            ClassDefinition selectedClass = null;
            for (int i = 0; i < ClassDefinitions.Length; i++)
            {
                if (ClassDefinitions[i].Name == _availableClasses[select - 1].itemName)
                {
                    selectedClass = ClassDefinitions[i];
                    break;
                }
            }
            _selection[treeIndex] = selectedClass;
            SubTrees[treeIndex].LoadClassDefinitionToUI(selectedClass);
            RefreshDropdownAvailableSelection();
        }

    }
}