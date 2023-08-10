using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Compares the property value to the value specified. Returns success if the values are the same.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=152")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class ComparePropertyValue : Conditional
{
	[Tooltip("The GameObject to compare the property of")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to compare the property of")]
	public SharedString componentName;

	[Tooltip("The name of the property")]
	public SharedString propertyName;

	[Tooltip("The value to compare to")]
	public SharedVariable compareValue;

	public override TaskStatus OnUpdate()
	{
		if (compareValue == null)
		{
			Debug.LogWarning((object)"Unable to compare field - compare value is null");
			return (TaskStatus)1;
		}
		Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(((SharedVariable<string>)componentName).Value);
		if (typeWithinAssembly == null)
		{
			Debug.LogWarning((object)"Unable to compare property - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to compare the property with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		object value = ((object)component).GetType().GetProperty(((SharedVariable<string>)propertyName).Value).GetValue(component, null);
		if (value == null && compareValue.GetValue() == null)
		{
			return (TaskStatus)2;
		}
		if (value.Equals(compareValue.GetValue()))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		componentName = null;
		propertyName = null;
		compareValue = null;
	}
}
