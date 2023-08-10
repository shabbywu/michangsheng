using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedVector2 : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedVector2 variable;

	[Tooltip("The variable to compare to")]
	public SharedVector2 compareTo;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Vector2 value = ((SharedVariable<Vector2>)variable).Value;
		if (((Vector2)(ref value)).Equals(((SharedVariable<Vector2>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		variable = Vector2.zero;
		compareTo = Vector2.zero;
	}
}
