using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour;

[TaskCategory("Basic/Behaviour")]
[TaskDescription("Enables/Disables the object. Returns Success.")]
public class SetIsEnabled : Action
{
	[Tooltip("The Object to use")]
	public SharedObject specifiedObject;

	[Tooltip("The enabled/disabled state")]
	public SharedBool enabled;

	public override TaskStatus OnUpdate()
	{
		if (specifiedObject == null && !(((SharedVariable<Object>)specifiedObject).Value is Behaviour))
		{
			Debug.LogWarning((object)"SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
			return (TaskStatus)1;
		}
		Object value = ((SharedVariable<Object>)specifiedObject).Value;
		((Behaviour)((value is Behaviour) ? value : null)).enabled = ((SharedVariable<bool>)enabled).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		if (specifiedObject != null)
		{
			((SharedVariable<Object>)specifiedObject).Value = null;
		}
		enabled = false;
	}
}
