using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Invokes the specified method with the specified parameters. Can optionally store the return value. Returns success if the method was invoked.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=145")]
[TaskCategory("Reflection")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public class InvokeMethod : Action
{
	[Tooltip("The GameObject to invoke the method on")]
	public SharedGameObject targetGameObject;

	[Tooltip("The component to invoke the method on")]
	public SharedString componentName;

	[Tooltip("The name of the method")]
	public SharedString methodName;

	[Tooltip("The first parameter of the method")]
	public SharedVariable parameter1;

	[Tooltip("The second parameter of the method")]
	public SharedVariable parameter2;

	[Tooltip("The third parameter of the method")]
	public SharedVariable parameter3;

	[Tooltip("The fourth parameter of the method")]
	public SharedVariable parameter4;

	[Tooltip("Store the result of the invoke call")]
	public SharedVariable storeResult;

	public override TaskStatus OnUpdate()
	{
		Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(((SharedVariable<string>)componentName).Value);
		if (typeWithinAssembly == null)
		{
			Debug.LogWarning((object)"Unable to invoke - type is null");
			return (TaskStatus)1;
		}
		Component component = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).GetComponent(typeWithinAssembly);
		if ((Object)(object)component == (Object)null)
		{
			Debug.LogWarning((object)("Unable to invoke method with component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		List<object> list = new List<object>();
		List<Type> list2 = new List<Type>();
		SharedVariable val = null;
		for (int i = 0; i < 4; i++)
		{
			if ((val = (SharedVariable)/*isinst with value type is only supported in some contexts*/) == null)
			{
				break;
			}
			list.Add(val.GetValue());
			list2.Add(((object)val).GetType().GetProperty("Value").PropertyType);
		}
		MethodInfo method = ((object)component).GetType().GetMethod(((SharedVariable<string>)methodName).Value, list2.ToArray());
		if (method == null)
		{
			Debug.LogWarning((object)("Unable to invoke method " + ((SharedVariable<string>)methodName).Value + " on component " + ((SharedVariable<string>)componentName).Value));
			return (TaskStatus)1;
		}
		object value = method.Invoke(component, list.ToArray());
		if (storeResult != null)
		{
			storeResult.SetValue(value);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		componentName = null;
		methodName = null;
		parameter1 = null;
		parameter2 = null;
		parameter3 = null;
		parameter4 = null;
		storeResult = null;
	}
}
