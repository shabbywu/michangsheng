using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Stores the right vector value.")]
public class GetRightVector : Action
{
	[Tooltip("The stored result")]
	[RequiredField]
	public SharedVector2 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((SharedVariable<Vector2>)storeResult).Value = Vector2.right;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		storeResult = Vector2.zero;
	}
}
