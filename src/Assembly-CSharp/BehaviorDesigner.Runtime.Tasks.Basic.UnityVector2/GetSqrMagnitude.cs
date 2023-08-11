using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Stores the square magnitude of the Vector2.")]
public class GetSqrMagnitude : Action
{
	[Tooltip("The Vector2 to get the square magnitude of")]
	public SharedVector2 vector2Variable;

	[Tooltip("The square magnitude of the vector")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SharedFloat sharedFloat = storeResult;
		Vector2 value = ((SharedVariable<Vector2>)vector2Variable).Value;
		((SharedVariable<float>)sharedFloat).Value = ((Vector2)(ref value)).sqrMagnitude;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		vector2Variable = Vector2.zero;
		storeResult = 0f;
	}
}
