using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedGameObject : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedGameObject variable;

	[Tooltip("The variable to compare to")]
	public SharedGameObject compareTo;

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)((SharedVariable<GameObject>)variable).Value == (Object)null && (Object)(object)((SharedVariable<GameObject>)compareTo).Value != (Object)null)
		{
			return (TaskStatus)1;
		}
		if ((Object)(object)((SharedVariable<GameObject>)variable).Value == (Object)null && (Object)(object)((SharedVariable<GameObject>)compareTo).Value == (Object)null)
		{
			return (TaskStatus)2;
		}
		if (((object)((SharedVariable<GameObject>)variable).Value).Equals((object?)((SharedVariable<GameObject>)compareTo).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		variable = null;
		compareTo = null;
	}
}
