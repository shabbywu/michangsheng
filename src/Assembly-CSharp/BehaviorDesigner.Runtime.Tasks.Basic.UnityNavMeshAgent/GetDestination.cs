using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent;

[TaskCategory("Basic/NavMeshAgent")]
[TaskDescription("Gets the destination of the agent in world-space units. Returns Success.")]
public class GetDestination : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[SharedRequired]
	[Tooltip("The NavMeshAgent destination")]
	public SharedVector3 storeValue;

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
		((SharedVariable<Vector3>)storeValue).Value = navMeshAgent.destination;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		storeValue = Vector3.zero;
	}
}
