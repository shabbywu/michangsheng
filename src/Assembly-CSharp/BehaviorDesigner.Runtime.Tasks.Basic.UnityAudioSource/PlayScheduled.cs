using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource;

[TaskCategory("Basic/AudioSource")]
[TaskDescription("Plays the audio clip with a delay specified in seconds. Returns Success.")]
public class PlayScheduled : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("Time in seconds on the absolute time-line that AudioSettings.dspTime refers to for when the sound should start playing")]
	public SharedFloat time = 0f;

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
		audioSource.PlayScheduled((double)((SharedVariable<float>)time).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		time = 0f;
	}
}
