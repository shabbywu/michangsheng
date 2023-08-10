using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedQuaternion : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedQuaternion variable;

	[Tooltip("The variable to compare to")]
	public SharedQuaternion compareTo;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Quaternion value = ((SharedVariable<Quaternion>)variable).Value;
		if (((Quaternion)(ref value)).Equals(((SharedVariable<Quaternion>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		variable = Quaternion.identity;
		compareTo = Quaternion.identity;
	}
}
