using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math;

[TaskCategory("Basic/Math")]
[TaskDescription("Lerp the float by an amount.")]
public class Lerp : Action
{
	[Tooltip("The from value")]
	public SharedFloat fromValue;

	[Tooltip("The to value")]
	public SharedFloat toValue;

	[Tooltip("The amount to lerp")]
	public SharedFloat lerpAmount;

	[Tooltip("The lerp resut")]
	[RequiredField]
	public SharedFloat storeResult;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<float>)storeResult).Value = Mathf.Lerp(((SharedVariable<float>)fromValue).Value, ((SharedVariable<float>)toValue).Value, ((SharedVariable<float>)lerpAmount).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		fromValue = (toValue = (lerpAmount = (storeResult = 0f)));
	}
}
