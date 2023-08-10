using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;

[TaskCategory("Basic/Vector3")]
[TaskDescription("Sets the X, Y, and Z values of the Vector3.")]
public class SetXYZ : Action
{
	[Tooltip("The Vector3 to set the values of")]
	public SharedVector3 vector3Variable;

	[Tooltip("The X value. Set to None to have the value ignored")]
	public SharedFloat xValue;

	[Tooltip("The Y value. Set to None to have the value ignored")]
	public SharedFloat yValue;

	[Tooltip("The Z value. Set to None to have the value ignored")]
	public SharedFloat zValue;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 value = ((SharedVariable<Vector3>)vector3Variable).Value;
		if (!((SharedVariable)xValue).IsNone)
		{
			value.x = ((SharedVariable<float>)xValue).Value;
		}
		if (!((SharedVariable)yValue).IsNone)
		{
			value.y = ((SharedVariable<float>)yValue).Value;
		}
		if (!((SharedVariable)zValue).IsNone)
		{
			value.z = ((SharedVariable<float>)zValue).Value;
		}
		((SharedVariable<Vector3>)vector3Variable).Value = value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		vector3Variable = Vector3.zero;
		xValue = (yValue = (zValue = 0f));
	}
}
