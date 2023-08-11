using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource;

[TaskCategory("Basic/AudioSource")]
[TaskDescription("Stops playing the audio clip. Returns Success.")]
public class Stop : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

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
		audioSource.Stop();
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
	}
}
