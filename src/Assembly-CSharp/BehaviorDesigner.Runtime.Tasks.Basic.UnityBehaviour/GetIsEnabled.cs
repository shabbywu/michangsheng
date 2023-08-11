using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityBehaviour;

[TaskCategory("Basic/Behaviour")]
[TaskDescription("Stores the enabled state of the object. Returns Success.")]
public class GetIsEnabled : Action
{
	[Tooltip("The Object to use")]
	public SharedObject specifiedObject;

	[Tooltip("The enabled/disabled state")]
	[RequiredField]
	public SharedBool storeValue;

	public override TaskStatus OnUpdate()
	{
		if (specifiedObject == null && !(((SharedVariable<Object>)specifiedObject).Value is Behaviour))
		{
			Debug.LogWarning((object)"SpecifiedObject is null or not a subclass of UnityEngine.Behaviour");
			return (TaskStatus)1;
		}
		SharedBool sharedBool = storeValue;
		Object value = ((SharedVariable<Object>)specifiedObject).Value;
		((SharedVariable<bool>)sharedBool).Value = ((Behaviour)((value is Behaviour) ? value : null)).enabled;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		if (specifiedObject != null)
		{
			((SharedVariable<Object>)specifiedObject).Value = null;
		}
		storeValue = false;
	}
}
