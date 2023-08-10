using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Blends the animation. Returns Success.")]
public class Blend : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the animation")]
	public SharedString animationName;

	[Tooltip("The weight the animation should blend to")]
	public float targetWeight = 1f;

	[Tooltip("The amount of time it takes to blend")]
	public float fadeLength = 0.3f;

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
		animation.Blend(((SharedVariable<string>)animationName).Value, targetWeight, fadeLength);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		animationName = "";
		targetWeight = 1f;
		fadeLength = 0.3f;
	}
}
