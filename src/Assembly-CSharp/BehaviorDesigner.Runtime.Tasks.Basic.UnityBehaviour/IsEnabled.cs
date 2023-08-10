using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour;

[TaskCategory("Basic/Behaviour")]
[TaskDescription("Returns Success if the object is enabled, otherwise Failure.")]
public class IsEnabled : Conditional
{
	[Tooltip("The Object to use")]
	public SharedObject specifiedObject;

	public override TaskStatus OnUpdate()
	{
		if (specifiedObject == null && !(((SharedVariable<Object>)specifiedObject).Value is Behaviour))
		{
			Debug.LogWarning((object)"SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
			return (TaskStatus)1;
		}
		Object value = ((SharedVariable<Object>)specifiedObject).Value;
		if (((Behaviour)((value is Behaviour) ? value : null)).enabled)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		if (specifiedObject != null)
		{
			((SharedVariable<Object>)specifiedObject).Value = null;
		}
	}
}
