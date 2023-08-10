using UnityEngine;

namespace Fungus;

[EventHandlerInfo("Scene", "Message Received", "The block will execute when the specified message is received from a Send Message command.")]
[AddComponentMenu("")]
public class MessageReceived : EventHandler
{
	[Tooltip("Fungus message to listen for")]
	[SerializeField]
	protected string message = "";

	public void OnSendFungusMessage(string message)
	{
		if (this.message == message)
		{
			ExecuteBlock();
		}
	}

	public override string GetSummary()
	{
		return message;
	}
}
