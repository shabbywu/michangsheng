using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
public class SetSharedTransformList : Action
{
	[Tooltip("The value to set the SharedTransformList to.")]
	public SharedTransformList targetValue;

	[RequiredField]
	[Tooltip("The SharedTransformList to set")]
	public SharedTransformList targetVariable;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<List<Transform>>)targetVariable).Value = ((SharedVariable<List<Transform>>)targetValue).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetValue = null;
		targetVariable = null;
	}
}
