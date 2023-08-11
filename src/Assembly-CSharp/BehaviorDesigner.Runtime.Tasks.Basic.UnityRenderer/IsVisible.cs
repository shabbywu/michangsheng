using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer;

[TaskCategory("Basic/Renderer")]
[TaskDescription("Returns Success if the Renderer is visible, otherwise Failure.")]
public class IsVisible : Conditional
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	private Renderer renderer;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			renderer = defaultGameObject.GetComponent<Renderer>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)renderer == (Object)null)
		{
			Debug.LogWarning((object)"Renderer is null");
			return (TaskStatus)1;
		}
		if (renderer.isVisible)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		targetGameObject = null;
	}
}
