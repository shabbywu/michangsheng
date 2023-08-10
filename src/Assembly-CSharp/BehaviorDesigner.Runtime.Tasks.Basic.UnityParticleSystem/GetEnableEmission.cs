using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityParticleSystem;

[TaskCategory("Basic/ParticleSystem")]
[TaskDescription("Stores if the Particle System is emitting particles.")]
public class GetEnableEmission : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Is the Particle System emitting particles?")]
	[RequiredField]
	public SharedBool storeResult;

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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)particleSystem == (Object)null)
		{
			Debug.LogWarning((object)"ParticleSystem is null");
			return (TaskStatus)1;
		}
		SharedBool sharedBool = storeResult;
		EmissionModule emission = particleSystem.emission;
		((SharedVariable<bool>)sharedBool).Value = ((EmissionModule)(ref emission)).enabled;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeResult = false;
	}
}
