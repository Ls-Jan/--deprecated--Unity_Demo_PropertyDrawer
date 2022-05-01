using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


//[CustomPropertyDrawer(typeof(Property_1))]
//public class Editor_Property_1 : PropertyDrawer {
//    public SerializedProperty sp_num=null;
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//        EditorGUI.BeginProperty(position, label, property);
//        if (sp_num == null) 
//            sp_num = property.FindPropertyRelative("num");
//        EditorGUI.PropertyField(position,sp_num);
//
//
//        EditorGUI.EndProperty();
//    }
//
//}


[CustomPropertyDrawer(typeof(Property_1))]
public class Editor_Property_1 : PropertyDrawer {
    public Dictionary<string, SerializedProperty> data = new Dictionary<string, SerializedProperty>();
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        if (data.TryGetValue(property.propertyPath, out SerializedProperty sp) == false) {
            sp = property.FindPropertyRelative("num");
            data.Add(property.propertyPath, sp);
        }
        EditorGUI.PropertyField(position, sp);


        EditorGUI.EndProperty();
    }

}