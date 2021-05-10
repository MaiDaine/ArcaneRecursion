using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ArcaneRecursion
{
    public class AutoLayoutTarget : UIBehaviour, ILayoutSelfController
    {
        public RectTransform Target;

        public virtual void SetLayoutHorizontal()
        {
            UpdateRectTransform();
        }

        public virtual void SetLayoutVertical()
        {
            UpdateRectTransform();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateRectTransform();
        }
#endif

        protected override void OnRectTransformDimensionsChange()
        {
            UpdateRectTransform();
        }

        private void UpdateRectTransform()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Target.sizeDelta = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        }
    }
}