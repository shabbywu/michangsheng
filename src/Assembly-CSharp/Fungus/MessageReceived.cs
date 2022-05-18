using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200131F RID: 4895
	[EventHandlerInfo("Scene", "Message Received", "The block will execute when the specified message is received from a Send Message command.")]
	[AddComponentMenu("")]
	public class MessageReceived : EventHandler
	{
		// Token: 0x06007743 RID: 30531 RVA: 0x00051400 File Offset: 0x0004F600
		public void OnSendFungusMessage(string message)
		{
			if (this.message == message)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x00051417 File Offset: 0x0004F617
		public override string GetSummary()
		{
			return this.message;
		}

		// Token: 0x040067F2 RID: 26610
		[Tooltip("Fungus message to listen for")]
		[SerializeField]
		protected string message = "";
	}
}
