using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedTransformList : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedTransformList variable;

	[Tooltip("The variable to compare to")]
	public SharedTransformList compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<List<Transform>>)variable).Value == null && ((SharedVariable<List<Transform>>)compareTo).Value != null)
		{
			return (TaskStatus)1;
		}
		if (((SharedVariable<List<Transform>>)variable).Value == null && ((SharedVariable<List<Transform>>)compareTo).Value == null)
		{
			return (TaskStatus)2;
		}
		if (((SharedVariable<List<Transform>>)variable).Value.Count == ((SharedVariable<List<Transform>>)compareTo).Value.Count)
		{
			for (int i = 0; i < ((SharedVariable<List<Transform>>)variable).Value.Count; i++)
			{
				if ((Object)(object)((SharedVariable<List<Transform>>)variable).Value[i] != (Object)(object)((SharedVariable<List<Transform>>)compareTo).Value[i])
				{
					return (TaskStatus)1;
				}
			}
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
