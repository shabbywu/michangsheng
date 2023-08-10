using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Performs a math operation on two Vector2s: Add, Subtract, Multiply, Divide, Min, or Max.")]
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

	[Tooltip("The first Vector2")]
	public SharedVector2 firstVector2;

	[Tooltip("The second Vector2")]
	public SharedVector2 secondVector2;

	[Tooltip("The variable to store the result")]
	public SharedVector2 storeResult;

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
			((SharedVariable<Vector2>)storeResult).Value = ((SharedVariable<Vector2>)firstVector2).Value + ((SharedVariable<Vector2>)secondVector2).Value;
			break;
		case Operation.Subtract:
			((SharedVariable<Vector2>)storeResult).Value = ((SharedVariable<Vector2>)firstVector2).Value - ((SharedVariable<Vector2>)secondVector2).Value;
			break;
		case Operation.Scale:
			((SharedVariable<Vector2>)storeResult).Value = Vector2.Scale(((SharedVariable<Vector2>)firstVector2).Value, ((SharedVariable<Vector2>)secondVector2).Value);
			break;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		operation = Operation.Add;
		firstVector2 = (secondVector2 = (storeResult = Vector2.zero));
	}
}
