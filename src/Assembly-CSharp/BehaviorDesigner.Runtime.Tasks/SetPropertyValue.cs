using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Sets the property to the value specified. Returns success if the property was set.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=150")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class SetPropertyValue : Action
{
	[Tooltip("The GameObject to set the property on")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to set the property on")]
	public SharedString componentName;

	[Tooltip("The name of the property")]
	public SharedString propertyName;

	[Tooltip("The value to set")]
	public SharedVariable propertyValue;

	public override TaskStatus OnUpdate()
	{
		if (propertyValue == null)
		{
			Debug.LogWarning((object)"Unable to get field - field value is null");
			return (TaskStatus)1;
		}
		Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(((SharedVariable<string>)componentName).Value);
		if (typeWithinAssembly == null)
		{
			Debug.LogWarning((object)"Unable to set property - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to set the property with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		((object)component).GetType().GetProperty(((SharedVariable<string>)propertyName).Value).SetValue(component, propertyValue.GetValue(), null);
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
