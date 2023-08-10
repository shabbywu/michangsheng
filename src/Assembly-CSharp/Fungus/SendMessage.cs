using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Flow", "Send Message", "Sends a message to either the owner Flowchart or all Flowcharts in the scene. Blocks can listen for this message using a Message Received event handler.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class SendMessage : Command
{
	[Tooltip("Target flowchart(s) to send the message to")]
	[SerializeField]
	protected MessageTarget messageTarget;

	[Tooltip("Name of the message to send")]
	[SerializeField]
	protected StringData _message = new StringData("");

	[HideInInspector]
	[FormerlySerializedAs("message")]
	public string messageOLD = "";

	public override void OnEnter()
	{
		if (_message.Value.Length == 0)
		{
			Continue();
			return;
		}
		MessageReceived[] array = null;
		array = ((messageTarget != 0) ? Object.FindObjectsOfType<MessageReceived>() : ((Component)this).GetComponents<MessageReceived>());
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnSendFungusMessage(_message.Value);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (_message.Value.Length == 0)
		{
			return "Error: No message specified";
		}
		return _message.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_message.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (messageOLD != "")
		{
			_message.Value = messageOLD;
			messageOLD = "";
		}
	}
}
