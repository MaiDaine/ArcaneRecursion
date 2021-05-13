using System.Collections.Generic;
using UnityEngine;

//TODO DYNAMIC LINK
namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Classes/EffectLibrary")]
    public class StatusLibrary : ScriptableObject
    {
        public List<NodeDefinition> StatusList;
        public NodeDefinition NotImplemented;

        public NodeDefinition GetEffectDefinition(string name)
        {
            NodeDefinition status = StatusList.Find(e => e.Name == name);

            return status ?? NotImplemented;
        }
    }
}
