using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MenuItem))]
public class MenuItemDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);
    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);

    var indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;

    int width = (int) (position.width * 0.5f);
    Rect foodRect = new Rect(position.x, position.y, width, position.height);
    Rect weightRect = new Rect(position.x + width + 20, position.y, position.width - width - 20, position.height);

    EditorGUI.PropertyField(foodRect, property.FindPropertyRelative("food"), GUIContent.none);
    EditorGUI.PropertyField(weightRect, property.FindPropertyRelative("weight"), GUIContent.none);

    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }
}
