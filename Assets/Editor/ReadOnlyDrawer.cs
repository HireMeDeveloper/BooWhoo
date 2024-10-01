using UnityEditor;
using UnityEngine;
using System;

/// <summary>
/// Property drawer for the ReadOnly attribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
	{
		string valueStr;

		// Depending on the property type, get the value as a string
		switch (prop.propertyType)
		{
			case SerializedPropertyType.Integer:
				valueStr = prop.intValue.ToString();
				break;
			case SerializedPropertyType.Boolean:
				valueStr = prop.boolValue.ToString();
				break;
			case SerializedPropertyType.Float:
				valueStr = prop.floatValue.ToString("0.00000");
				break;
			case SerializedPropertyType.String:
				valueStr = prop.stringValue;
				break;

			default:
				valueStr = "(not supported)";
				break;
		}

		EditorGUI.LabelField(position, label.text, valueStr);
	}

}
