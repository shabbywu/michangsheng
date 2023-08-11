using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedObjectList : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedObjectList variable;

	[Tooltip("The variable to compare to")]
	public SharedObjectList compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<List<Object>>)variable).Value == null && ((SharedVariable<List<Object>>)compareTo).Value != null)
		{
			return (TaskStatus)1;
		}
		if (((SharedVariable<List<Object>>)variable).Value == null && ((SharedVariable<List<Object>>)compareTo).Value == null)
		{
			return (TaskStatus)2;
		}
		if (((SharedVariable<List<Object>>)variable).Value.Count == ((SharedVariable<List<Object>>)compareTo).Value.Count)
		{
			for (int i = 0; i < ((SharedVariable<List<Object>>)variable).Value.Count; i++)
			{
				if (((SharedVariable<List<Object>>)variable).Value[i] != ((SharedVariable<List<Object>>)compareTo).Value[i])
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
