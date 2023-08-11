using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Compares the field value to the value specified. Returns success if the values are the same.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=151")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class CompareFieldValue : Conditional
{
	[Tooltip("The GameObject to compare the field on")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to compare the field on")]
	public SharedString componentName;

	[Tooltip("The name of the field")]
	public SharedString fieldName;

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
			Debug.LogWarning((object)"Unable to compare field - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to compare the field with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		object value = ((object)component).GetType().GetField(((SharedVariable<string>)fieldName).Value).GetValue(component);
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
		fieldName = null;
		compareValue = null;
	}
}
