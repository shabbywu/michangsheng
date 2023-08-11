using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("Basic/Animation")]
[TaskDescription("Cross fades an animation after previous animations has finished playing. Returns Success.")]
public class CrossFadeQueued : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the animation")]
	public SharedString animationName;

	[Tooltip("The amount of time it takes to blend")]
	public float fadeLength = 0.3f;

	[Tooltip("Specifies when the animation should start playing")]
	public QueueMode queue;

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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)animation == (Object)null)
		{
			Debug.LogWarning((object)"Animation is null");
			return (TaskStatus)1;
		}
		animation.CrossFadeQueued(((SharedVariable<string>)animationName).Value, fadeLength, queue, playMode);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		((SharedVariable<string>)animationName).Value = "";
		fadeLength = 0.3f;
		queue = (QueueMode)0;
		playMode = (PlayMode)0;
	}
}
