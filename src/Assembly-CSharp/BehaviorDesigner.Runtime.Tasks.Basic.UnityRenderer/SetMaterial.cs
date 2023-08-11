using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRenderer;

[TaskCategory("Basic/Renderer")]
[TaskDescription("Sets the material on the Renderer.")]
public class SetMaterial : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The material to set")]
	public SharedMaterial material;

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
		renderer.material = ((SharedVariable<Material>)material).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		material = null;
	}
}
