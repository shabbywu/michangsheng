using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Sets the wrap mode to the specified value. Returns Success.")]
public class SetWrapMode : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("How should time beyond the playback range of the clip be treated?")]
	public WrapMode wrapMode;

	private Animation animation;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			animation = defaultGameObject.GetComponent<Animation>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)animation == (Object)null)
		{
			Debug.LogWarning((object)"Animation is null");
			return (TaskStatus)1;
		}
		animation.wrapMode = wrapMode;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		wrapMode = (WrapMode)0;
	}
}
