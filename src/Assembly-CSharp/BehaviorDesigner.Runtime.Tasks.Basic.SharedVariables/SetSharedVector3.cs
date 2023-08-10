using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedVector3 variable to the specified object. Returns Success.")]
public class SetSharedVector3 : Action
{
	[Tooltip("The value to set the SharedVector3 to")]
	public SharedVector3 targetValue;

	[RequiredField]
	[Tooltip("The SharedVector3 to set")]
	public SharedVector3 targetVariable;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector3>)targetVariable).Value = ((SharedVariable<Vector3>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		targetValue = Vector3.zero;
		targetVariable = Vector3.zero;
	}
}
