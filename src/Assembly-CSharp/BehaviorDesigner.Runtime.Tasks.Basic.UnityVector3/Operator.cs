using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Performs a math operation on two Vector3s: Add, Subtract, Multiply, Divide, Min, or Max.")]
public class Operator : Action
{
	public enum Operation
	{
		Add,
		Subtract,
		Scale
	}

	[Tooltip("The operation to perform")]
	public Operation operation;

	[Tooltip("The first Vector3")]
	public SharedVector3 firstVector3;

	[Tooltip("The second Vector3")]
	public SharedVector3 secondVector3;

	[Tooltip("The variable to store the result")]
	public SharedVector3 storeResult;

	public override TaskStatus OnUpdate()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		switch (operation)
		{
		case Operation.Add:
			((SharedVariable<Vector3>)storeResult).Value = ((SharedVariable<Vector3>)firstVector3).Value + ((SharedVariable<Vector3>)secondVector3).Value;
			break;
		case Operation.Subtract:
			((SharedVariable<Vector3>)storeResult).Value = ((SharedVariable<Vector3>)firstVector3).Value - ((SharedVariable<Vector3>)secondVector3).Value;
			break;
		case Operation.Scale:
			((SharedVariable<Vector3>)storeResult).Value = Vector3.Scale(((SharedVariable<Vector3>)firstVector3).Value, ((SharedVariable<Vector3>)secondVector3).Value);
			break;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		operation = Operation.Add;
		firstVector3 = (secondVector3 = (storeResult = Vector3.zero));
	}
}
