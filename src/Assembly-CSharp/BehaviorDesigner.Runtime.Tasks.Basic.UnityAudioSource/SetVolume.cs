using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource;

[TaskCategory("Basic/AudioSource")]
[TaskDescription("Sets the volume value of the AudioSource. Returns Success.")]
public class SetVolume : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The volume value of the AudioSource")]
	public SharedFloat volume;

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
		if ((Object)(object)audioSource == (Object)null)
		{
			Debug.LogWarning((object)"AudioSource is null");
			return (TaskStatus)1;
		}
		audioSource.volume = ((SharedVariable<float>)volume).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		volume = 1f;
	}
}
