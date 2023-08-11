using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight;

[TaskCategory("Basic/Light")]
[TaskDescription("Sets the color of the light.")]
public class SetColor : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The color to set")]
	public SharedColor color;

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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)light == (Object)null)
		{
			Debug.LogWarning((object)"Light is null");
			return (TaskStatus)1;
		}
		light.color = ((SharedVariable<Color>)color).Value;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		color = Color.white;
	}
}
