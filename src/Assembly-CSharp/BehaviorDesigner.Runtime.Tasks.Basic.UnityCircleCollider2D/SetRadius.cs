using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCircleCollider2D;

[TaskCategory("Basic/CircleCollider2D")]
[TaskDescription("Sets the radius of the CircleCollider2D. Returns Success.")]
public class SetRadius : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The radius of the CircleCollider2D")]
	public SharedFloat radius;

	private CircleCollider2D circleCollider2D;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)circleCollider2D == (Object)null)
		{
			Debug.LogWarning((object)"CircleCollider2D is null");
			return (TaskStatus)1;
		}
		circleCollider2D.radius = ((SharedVariable<float>)radius).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		radius = 0f;
	}
}
