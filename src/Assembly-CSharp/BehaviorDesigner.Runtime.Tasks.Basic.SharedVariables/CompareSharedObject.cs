using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedObject : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedObject variable;

	[Tooltip("The variable to compare to")]
	public SharedObject compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<Object>)variable).Value == (Object)null && ((SharedVariable<Object>)compareTo).Value != (Object)null)
		{
			return (TaskStatus)1;
		}
		if (((SharedVariable<Object>)variable).Value == (Object)null && ((SharedVariable<Object>)compareTo).Value == (Object)null)
		{
			return (TaskStatus)2;
		}
		if (((object)((SharedVariable<Object>)variable).Value).Equals((object?)((SharedVariable<Object>)compareTo).Value))
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
