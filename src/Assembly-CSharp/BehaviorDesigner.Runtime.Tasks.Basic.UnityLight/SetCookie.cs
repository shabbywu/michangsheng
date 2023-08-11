using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight;

[TaskCategory("Basic/Light")]
[TaskDescription("Sets the cookie of the light.")]
public class SetCookie : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The cookie to set")]
	public Texture2D cookie;

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
		light.cookie = (Texture)(object)cookie;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		cookie = null;
	}
}
