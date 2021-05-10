using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ArcaneRecursion
{
    public class UIStatus : MonoBehaviour, IPointerEnterHandler
    {
        private NodeDefinition _spellDef;

        public void UpdateState(NodeDefinition spellDef)
        {
            _spellDef = spellDef;
            GetComponent<Image>().sprite = spellDef.Icon;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("TODO TOOLTIP " + _spellDef);
        }
    }
}