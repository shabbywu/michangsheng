using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedObject variable to the specified object. Returns Success.")]
public class SetSharedObject : Action
{
	[Tooltip("The value to set the SharedObject to")]
	public SharedObject targetValue;

	[RequiredField]
	[Tooltip("The SharedTransform to set")]
	public SharedObject targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<Object>)targetVariable).Value = ((SharedVariable<Object>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = null;
		targetVariable = null;
	}
}
