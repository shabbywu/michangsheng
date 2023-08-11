using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight;

[TaskCategory("Basic/Light")]
[TaskDescription("Sets the culling mask of the light.")]
public class SetCullingMask : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The culling mask to set")]
	public LayerMask cullingMask;

	private Light light;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			light = defaultGameObject.GetComponent<Light>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if ((Object)(object)light == (Object)null)
		{
			Debug.LogWarning((object)"Light is null");
			return (TaskStatus)1;
		}
		light.cullingMask = ((LayerMask)(ref cullingMask)).value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		cullingMask = LayerMask.op_Implicit(-1);
	}
}
