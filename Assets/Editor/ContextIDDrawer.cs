using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(ContextIDAttribute))]
public class ContextIDDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string[] guids = AssetDatabase.FindAssets("t:UINavigationRules");
        if (guids.Length == 0)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        UINavigationRules rules = AssetDatabase.LoadAssetAtPath<UINavigationRules>(path);

        string[] options = rules.Rules.Select(r => r.ContextID).ToArray();

        int currentIndex = Mathf.Max(0, System.Array.IndexOf(options, property.stringValue));

        currentIndex = EditorGUI.Popup(position, label.text, currentIndex, options);

        property.stringValue = options.Length > 0 ? options[currentIndex] : "";
    }
}