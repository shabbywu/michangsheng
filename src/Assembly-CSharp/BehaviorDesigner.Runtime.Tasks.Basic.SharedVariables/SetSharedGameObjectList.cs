using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedGameObjectList variable to the specified object. Returns Success.")]
public class SetSharedGameObjectList : Action
{
	[Tooltip("The value to set the SharedGameObjectList to.")]
	public SharedGameObjectList targetValue;

	[RequiredField]
	[Tooltip("The SharedGameObjectList to set")]
	public SharedGameObjectList targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<List<GameObject>>)targetVariable).Value = ((SharedVariable<List<GameObject>>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = null;
		targetVariable = null;
	}
}
