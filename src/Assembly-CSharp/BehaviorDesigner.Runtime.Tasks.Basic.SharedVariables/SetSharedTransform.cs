using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedTransform variable to the specified object. Returns Success.")]
public class SetSharedTransform : Action
{
	[Tooltip("The value to set the SharedTransform to. If null the variable will be set to the current Transform")]
	public SharedTransform targetValue;

	[RequiredField]
	[Tooltip("The SharedTransform to set")]
	public SharedTransform targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<Transform>)targetVariable).Value = (((Object)(object)((SharedVariable<Transform>)targetValue).Value != (Object)null) ? ((SharedVariable<Transform>)targetValue).Value : ((Task)this).transform);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = null;
		targetVariable = null;
	}
}
