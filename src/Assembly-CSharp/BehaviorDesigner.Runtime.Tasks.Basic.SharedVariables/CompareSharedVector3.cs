using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedVector3 : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedVector3 variable;

	[Tooltip("The variable to compare to")]
	public SharedVector3 compareTo;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Vector3 value = ((SharedVariable<Vector3>)variable).Value;
		if (((Vector3)(ref value)).Equals(((SharedVariable<Vector3>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		variable = Vector3.zero;
		compareTo = Vector3.zero;
	}
}
