using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//[CustomPropertyDrawer(typeof(Property_2))]
//class XJEditor_LimitedValue : PropertyDrawer {
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//        //在该数据是被嵌套其中的时候该数据的绘制出的所有元素会发生“错位”、“被压缩”问题。
//        //先说一下“被压缩”的原因——因为展开时显示的内容要向右移动一点以形成错位让排版好看，这“右移”其实是个人都能理解，但问题在于，它的实现手段非常有问题
//        //它的“右移”实际上是把你绘制的所有元素的绘制区的左x值增大(但右x值不变)，从而造成“被压缩”问题
//        if (label == GUIContent.none)
//            label = new GUIContent(" ");//让它不为空，便于对齐
//        EditorGUI.BeginProperty(position, label, property);
//        position = EditorGUI.PrefixLabel(position, label);//显示字段标签，返回标签占用后剩余的区域
//
//        float width = position.width / 5;//将宽度分为5段，多出的两段空间用于放Label，以及留白
//        position.width = width;
//        foreach (var str in new List<string> { "(", "min", ",", "max", ")", " ", "curr", "" }) {
//            if (str.Length < 3) {//符号Label
//                EditorGUI.LabelField(position, str, EditorStyles.boldLabel);
//                position.x += width / 5;
//            }
//            else {//变量Property
//                var val = property.FindPropertyRelative(str);
//                val.intValue = EditorGUI.IntField(position, val.intValue);
//                position.x += width;
//            }
//        }
//        EditorGUI.EndProperty();
//    }
//}
//
//


[CustomPropertyDrawer(typeof(Property_2))]
class XJEditor_LimitedValue : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //先说一下“被压缩”的原因——因为展开时显示的内容要向右移动一点以形成错位让排版好看，这“右移”其实是个人都能理解，但问题在于，它的实现手段非常有问题
        //它的“右移”实际上是把你绘制的所有元素的绘制区的左x值增大(但右x值不变)，从而造成“被压缩”问题
        //尝试了几个小时，最终看到了个property.depth解决了当前问题，只不过多级嵌套我不知道会不会出问题
        if (label == GUIContent.none)
            label = new GUIContent(" ");//让它不为空，便于对齐
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);//显示字段标签，返回标签占用后剩余的区域

        float offset = 15;//因为被嵌套的数据在显示时会被强制右移，为了应对这情况要提前修正数据。其中"15"为魔法数字
        if (property.depth != 0) //【【【说明本数据是被嵌套显示的】】】
            position.x -= offset;

        float width = position.width / 5;//将宽度分为5段，多出的两段空间用于放Label，以及留白
        position.width = width;
        foreach (var str in new List<string> { "(", "min", ",", "max", ")", " ", "curr", "" }) {
            if (str.Length < 3) {//符号Label
                EditorGUI.LabelField(position, str, EditorStyles.boldLabel);
                position.x += width / 5;
            }
            else {//变量Property
                if (property.depth != 0) {
                    position.width = width + offset;
                }
                var val = property.FindPropertyRelative(str);
                val.intValue = EditorGUI.IntField(position, val.intValue);
                position.x += width;
            }
        }
        EditorGUI.EndProperty();
    }
}

