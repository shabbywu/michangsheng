using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight;

[TaskCategory("Basic/Light")]
[TaskDescription("Sets the shadow strength of the light.")]
public class SetShadowSoftnessStrength : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The shadow strength to set")]
	public SharedFloat shadowStrength;

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
		light.shadowStrength = ((SharedVariable<float>)shadowStrength).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		shadowStrength = 0f;
	}
}
