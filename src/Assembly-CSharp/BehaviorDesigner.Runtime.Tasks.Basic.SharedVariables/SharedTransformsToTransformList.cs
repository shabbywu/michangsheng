using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedTransformList values from the Transforms. Returns Success.")]
public class SharedTransformsToTransformList : Action
{
	[Tooltip("The Transforms value")]
	public SharedTransform[] transforms;

	[RequiredField]
	[Tooltip("The SharedTransformList to set")]
	public SharedTransformList storedTransformList;

	public override void OnAwake()
	{
		((SharedVariable<List<Transform>>)storedTransformList).Value = new List<Transform>();
	}

	public override TaskStatus OnUpdate()
	{
		if (transforms != null && transforms.Length != 0)
		{
			((SharedVariable<List<Transform>>)storedTransformList).Value.Clear();
			for (int i = 0; i < transforms.Length; i++)
			{
				((SharedVariable<List<Transform>>)storedTransformList).Value.Add(((SharedVariable<Transform>)transforms[i]).Value);
			}
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		transforms = null;
		storedTransformList = null;
	}
}
