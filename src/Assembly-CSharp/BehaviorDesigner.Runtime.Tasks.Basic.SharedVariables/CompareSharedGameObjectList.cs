using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
public class CompareSharedGameObjectList : Conditional
{
	[Tooltip("The first variable to compare")]
	public SharedGameObjectList variable;

	[Tooltip("The variable to compare to")]
	public SharedGameObjectList compareTo;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<List<GameObject>>)variable).Value == null && ((SharedVariable<List<GameObject>>)compareTo).Value != null)
		{
			return (TaskStatus)1;
		}
		if (((SharedVariable<List<GameObject>>)variable).Value == null && ((SharedVariable<List<GameObject>>)compareTo).Value == null)
		{
			return (TaskStatus)2;
		}
		if (((SharedVariable<List<GameObject>>)variable).Value.Count == ((SharedVariable<List<GameObject>>)compareTo).Value.Count)
		{
			for (int i = 0; i < ((SharedVariable<List<GameObject>>)variable).Value.Count; i++)
			{
				if ((Object)(object)((SharedVariable<List<GameObject>>)variable).Value[i] != (Object)(object)((SharedVariable<List<GameObject>>)compareTo).Value[i])
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
