using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent;

[TaskCategory("Basic/NavMeshAgent")]
[TaskDescription("Sets the maximum movement speed when following a path. Returns Success.")]
public class SetSpeed : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The NavMeshAgent speed")]
	public SharedFloat speed;

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
		if ((Object)(object)navMeshAgent == (Object)null)
		{
			Debug.LogWarning((object)"NavMeshAgent is null");
			return (TaskStatus)1;
		}
		navMeshAgent.speed = ((SharedVariable<float>)speed).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		speed = 0f;
	}
}
