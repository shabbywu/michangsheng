using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Sets the field to the value specified. Returns success if the field was set.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=149")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class SetFieldValue : Action
{
	[Tooltip("The GameObject to set the field on")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to set the field on")]
	public SharedString componentName;

	[Tooltip("The name of the field")]
	public SharedString fieldName;

	[Tooltip("The value to set")]
	public SharedVariable fieldValue;

	public override TaskStatus OnUpdate()
	{
		if (fieldValue == null)
		{
			Debug.LogWarning((object)"Unable to get field - field value is null");
			return (TaskStatus)1;
		}
		Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(((SharedVariable<string>)componentName).Value);
		if (typeWithinAssembly == null)
		{
			Debug.LogWarning((object)"Unable to set field - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to set the field with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		((object)component).GetType().GetField(((SharedVariable<string>)fieldName).Value).SetValue(component, fieldValue.GetValue());
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		componentName = null;
		fieldName = null;
		fieldValue = null;
	}
}
