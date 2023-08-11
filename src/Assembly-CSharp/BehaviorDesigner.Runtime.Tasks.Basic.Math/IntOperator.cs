using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Performs a math operation on two integers: Add, Subtract, Multiply, Divide, Min, or Max.")]
public class IntOperator : Action
{
	public enum Operation
	{
		Add,
		Subtract,
		Multiply,
		Divide,
		Min,
		Max,
		Modulo
	}

	[Tooltip("The operation to perform")]
	public Operation operation;

	[Tooltip("The first integer")]
	public SharedInt integer1;

	[Tooltip("The second integer")]
	public SharedInt integer2;

	[RequiredField]
	[Tooltip("The variable to store the result")]
	public SharedInt storeResult;

	public override TaskStatus OnUpdate()
	{
		switch (operation)
		{
		case Operation.Add:
			((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)integer1).Value + ((SharedVariable<int>)integer2).Value;
			break;
		case Operation.Subtract:
			((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)integer1).Value - ((SharedVariable<int>)integer2).Value;
			break;
		case Operation.Multiply:
			((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)integer1).Value * ((SharedVariable<int>)integer2).Value;
			break;
		case Operation.Divide:
			((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)integer1).Value / ((SharedVariable<int>)integer2).Value;
			break;
		case Operation.Min:
			((SharedVariable<int>)storeResult).Value = Mathf.Min(((SharedVariable<int>)integer1).Value, ((SharedVariable<int>)integer2).Value);
			break;
		case Operation.Max:
			((SharedVariable<int>)storeResult).Value = Mathf.Max(((SharedVariable<int>)integer1).Value, ((SharedVariable<int>)integer2).Value);
			break;
		case Operation.Modulo:
			((SharedVariable<int>)storeResult).Value = ((SharedVariable<int>)integer1).Value % ((SharedVariable<int>)integer2).Value;
			break;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		operation = Operation.Add;
		((SharedVariable<int>)integer1).Value = 0;
		((SharedVariable<int>)integer2).Value = 0;
		((SharedVariable<int>)storeResult).Value = 0;
	}
}
