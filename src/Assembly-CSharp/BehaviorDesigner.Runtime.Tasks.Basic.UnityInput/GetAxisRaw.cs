using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput;

[TaskCategory("Basic/Input")]
[TaskDescription("Stores the raw value of the specified axis and stores it in a float.")]
public class GetAxisRaw : Action
{
	[Tooltip("The name of the axis")]
	public SharedString axisName;

	[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
	public SharedFloat multiplier;

	[RequiredField]
	[Tooltip("The stored result")]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		float num = Input.GetAxis(((SharedVariable<string>)axisName).Value);
		if (!((SharedVariable)multiplier).IsNone)
		{
			num *= ((SharedVariable<float>)multiplier).Value;
		}
		((SharedVariable<float>)storeResult).Value = num;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		axisName = "";
		multiplier = 1f;
		storeResult = 0f;
	}
}
