using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

[TaskCategory("Basic/GameObject")]
[TaskDescription("Sends a message to the target GameObject. Returns Success.")]
public class SendMessage : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The message to send")]
	public SharedString message;

	[Tooltip("The value to send")]
	public SharedGenericVariable value;

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<GenericVariable>)(object)value).Value != null)
		{
			((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).SendMessage(((SharedVariable<string>)message).Value, ((SharedVariable<GenericVariable>)(object)value).Value.value.GetValue());
		}
		else
		{
			((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).SendMessage(((SharedVariable<string>)message).Value);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		message = "";
	}
}
