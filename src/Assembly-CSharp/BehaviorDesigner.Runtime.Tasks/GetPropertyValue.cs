using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Gets the value from the property specified. Returns success if the property was retrieved.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=148")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class GetPropertyValue : Action
{
	[Tooltip("The GameObject to get the property of")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to get the property of")]
	public SharedString componentName;

	[Tooltip("The name of the property")]
	public SharedString propertyName;

	[Tooltip("The value of the property")]
	[RequiredField]
	public SharedVariable propertyValue;

	public override TaskStatus OnUpdate()
	{
		if (propertyValue == null)
		{
			Debug.LogWarning((object)"Unable to get property - property value is null");
			return (TaskStatus)1;
		}
		Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(((SharedVariable<string>)componentName).Value);
		if (typeWithinAssembly == null)
		{
			Debug.LogWarning((object)"Unable to get property - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to get the property with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		PropertyInfo property = ((object)component).GetType().GetProperty(((SharedVariable<string>)propertyName).Value);
		propertyValue.SetValue(property.GetValue(component, null));
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		componentName = null;
		propertyName = null;
		propertyValue = null;
	}
}
