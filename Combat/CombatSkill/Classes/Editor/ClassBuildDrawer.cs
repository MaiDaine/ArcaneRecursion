using UnityEngine;
using UnityEditor;

namespace ArcaneRecursion
{
    [CustomPropertyDrawer(typeof(ClassBuild))]
    public class ClassBuildDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ClassNames className = (ClassNames)property.FindPropertyRelative("Name").enumValueIndex;

            int indent = EditorGUI.indentLevel;
            Vector2 currentPos = new Vector2(position.x, position.y);
            EditorGUI.BeginProperty(position, label, property);
            if (property.isExpanded)
            {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, "", false);
                EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(className.ToString()));
                EditorGUI.indentLevel = 1;

                currentPos.y += 20;
                EditorGUI.PropertyField(new Rect(currentPos, new Vector2(270, 20)), property.FindPropertyRelative("Name"), new GUIContent("Class"));
                SerializedProperty availableSkills = property.FindPropertyRelative("AvailableSkills");
                EditorGUI.indentLevel = 2;
                if (className != ClassNames.Innate)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        currentPos.y += 20;
                        SerializedProperty value = availableSkills.GetArrayElementAtIndex(i);
                        EditorGUI.PropertyField(new Rect(currentPos, new Vector2(300, 20)), value, new GUIContent(ClassSkillLibrary.ClassSkillsDatas[className][i].Name));
                    }
                }
            }
            else
            {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, new GUIContent(className.ToString()), false);
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
            property.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
                return 160;
            else
                return 15;
        }
    }
}