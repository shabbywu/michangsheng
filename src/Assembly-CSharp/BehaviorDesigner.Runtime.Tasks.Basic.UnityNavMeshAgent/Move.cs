using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent;

[TaskCategory("Basic/NavMeshAgent")]
[TaskDescription("Apply relative movement to the current position. Returns Success.")]
public class Move : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The relative movement vector")]
	public SharedVector3 offset;

	private NavMeshAgent navMeshAgent;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)navMeshAgent == (Object)null)
		{
			Debug.LogWarning((object)"NavMeshAgent is null");
			return (TaskStatus)1;
		}
		navMeshAgent.Move(((SharedVariable<Vector3>)offset).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		offset = Vector3.zero;
	}
}
