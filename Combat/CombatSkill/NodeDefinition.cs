using UnityEngine;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Classes/NodeDefinition")]
    public class NodeDefinition : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
    }
}
