namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Performs a math operation on two bools: AND, OR, NAND, or XOR.")]
public class BoolOperator : Action
{
	public enum Operation
	{
		AND,
		OR,
		NAND,
		XOR
	}

	[Tooltip("The operation to perform")]
	public Operation operation;

	[Tooltip("The first bool")]
	public SharedBool bool1;

	[Tooltip("The second bool")]
	public SharedBool bool2;

	[Tooltip("The variable to store the result")]
	public SharedBool storeResult;

	public override TaskStatus OnUpdate()
	{
		switch (operation)
		{
		case Operation.AND:
			((SharedVariable<bool>)storeResult).Value = ((SharedVariable<bool>)bool1).Value && ((SharedVariable<bool>)bool2).Value;
			break;
		case Operation.OR:
			((SharedVariable<bool>)storeResult).Value = ((SharedVariable<bool>)bool1).Value || ((SharedVariable<bool>)bool2).Value;
			break;
		case Operation.NAND:
			((SharedVariable<bool>)storeResult).Value = !((SharedVariable<bool>)bool1).Value || !((SharedVariable<bool>)bool2).Value;
			break;
		case Operation.XOR:
			((SharedVariable<bool>)storeResult).Value = ((SharedVariable<bool>)bool1).Value ^ ((SharedVariable<bool>)bool2).Value;
			break;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		operation = Operation.AND;
		((SharedVariable<bool>)bool1).Value = false;
		((SharedVariable<bool>)bool2).Value = false;
		((SharedVariable<bool>)storeResult).Value = false;
	}
}
