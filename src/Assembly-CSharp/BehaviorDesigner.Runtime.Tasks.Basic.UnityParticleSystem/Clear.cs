using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem;

[TaskCategory("Basic/ParticleSystem")]
[TaskDescription("Clear the Particle System.")]
public class Clear : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

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
		particleSystem.Clear();
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
	}
}
