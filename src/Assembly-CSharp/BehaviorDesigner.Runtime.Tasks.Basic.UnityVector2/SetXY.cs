using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2;

[TaskCategory("Basic/Vector2")]
[TaskDescription("Sets the X and Y values of the Vector2.")]
public class SetXY : Action
{
	[Tooltip("The Vector2 to set the values of")]
	public SharedVector2 vector2Variable;

	[Tooltip("The X value. Set to None to have the value ignored")]
	public SharedFloat xValue;

	[Tooltip("The Y value. Set to None to have the value ignored")]
	public SharedFloat yValue;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Vector2 value = ((SharedVariable<Vector2>)vector2Variable).Value;
		if (!((SharedVariable)xValue).IsNone)
		{
			value.x = ((SharedVariable<float>)xValue).Value;
		}
		if (!((SharedVariable)yValue).IsNone)
		{
			value.y = ((SharedVariable<float>)yValue).Value;
		}
		((SharedVariable<Vector2>)vector2Variable).Value = value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		vector2Variable = Vector2.zero;
		xValue = (yValue = 0f);
	}
}
