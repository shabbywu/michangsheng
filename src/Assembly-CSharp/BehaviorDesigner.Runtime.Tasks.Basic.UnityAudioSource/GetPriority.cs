using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource;

[TaskCategory("Basic/AudioSource")]
[TaskDescription("Stores the priority value of the AudioSource. Returns Success.")]
public class GetPriority : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The priority value of the AudioSource")]
	[RequiredField]
	public SharedInt storeValue;

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
		((SharedVariable<int>)storeValue).Value = audioSource.priority;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		storeValue = 1;
	}
}
