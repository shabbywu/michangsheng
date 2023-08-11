using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent;

[TaskCategory("Basic/NavMeshAgent")]
[TaskDescription("Sets the destination of the agent in world-space units. Returns Success if the destination is valid.")]
public class SetDestination : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[SharedRequired]
	[Tooltip("The NavMeshAgent destination")]
	public SharedVector3 destination;

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
		if (navMeshAgent.SetDestination(((SharedVariable<Vector3>)destination).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		destination = Vector3.zero;
	}
}
