using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Plays animation without any blending. Returns Success.")]
public class Play : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the animation")]
	public SharedString animationName;

	[Tooltip("The play mode of the animation")]
	public PlayMode playMode;

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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)animation == (Object)null)
		{
			Debug.LogWarning((object)"Animation is null");
			return (TaskStatus)1;
		}
		if (string.IsNullOrEmpty(((SharedVariable<string>)animationName).Value))
		{
			animation.Play();
		}
		else
		{
			animation.Play(((SharedVariable<string>)animationName).Value, playMode);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		((SharedVariable<string>)animationName).Value = "";
		playMode = (PlayMode)0;
	}
}
