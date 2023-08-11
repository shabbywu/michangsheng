using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem;

[TaskCategory("Basic/ParticleSystem")]
[TaskDescription("Stores the time of the Particle System.")]
public class GetTime : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The time of the ParticleSystem")]
	[RequiredField]
	public SharedFloat storeResult;

	private ParticleSystem particleSystem;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			particleSystem = defaultGameObject.GetComponent<ParticleSystem>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)particleSystem == (Object)null)
		{
			Debug.LogWarning((object)"ParticleSystem is null");
			return (TaskStatus)1;
		}
		((SharedVariable<float>)storeResult).Value = particleSystem.time;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeResult = 0f;
	}
}
