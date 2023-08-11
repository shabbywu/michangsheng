using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Returns Success if the animation is currently playing.")]
public class IsPlaying : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the animation")]
	public SharedString animationName;

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
		if ((Object)(object)animation == (Object)null)
		{
			Debug.LogWarning((object)"Animation is null");
			return (TaskStatus)1;
		}
		if (string.IsNullOrEmpty(((SharedVariable<string>)animationName).Value))
		{
			if (animation.isPlaying)
			{
				return (TaskStatus)2;
			}
			return (TaskStatus)1;
		}
		if (animation.IsPlaying(((SharedVariable<string>)animationName).Value))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		((SharedVariable<string>)animationName).Value = "";
	}
}
