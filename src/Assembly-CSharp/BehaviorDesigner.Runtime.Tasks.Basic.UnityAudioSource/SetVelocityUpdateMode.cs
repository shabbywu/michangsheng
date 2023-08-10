using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource;

[TaskCategory("Basic/AudioSource")]
[TaskDescription("Sets the rolloff mode of the AudioSource. Returns Success.")]
public class SetVelocityUpdateMode : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The velocity update mode of the AudioSource")]
	public AudioVelocityUpdateMode velocityUpdateMode;

	private AudioSource audioSource;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			audioSource = defaultGameObject.GetComponent<AudioSource>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)audioSource == (Object)null)
		{
			Debug.LogWarning((object)"AudioSource is null");
			return (TaskStatus)1;
		}
		audioSource.velocityUpdateMode = velocityUpdateMode;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		velocityUpdateMode = (AudioVelocityUpdateMode)0;
	}
}
