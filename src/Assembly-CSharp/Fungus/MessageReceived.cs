using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA0 RID: 3744
	[EventHandlerInfo("Scene", "Message Received", "The block will execute when the specified message is received from a Send Message command.")]
	[AddComponentMenu("")]
	public class MessageReceived : EventHandler
	{
		// Token: 0x06006A0D RID: 27149 RVA: 0x00292928 File Offset: 0x00290B28
		public void OnSendFungusMessage(string message)
		{
			if (this.message == message)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0029293F File Offset: 0x00290B3F
		public override string GetSummary()
		{
			return this.message;
		}

		// Token: 0x040059D8 RID: 23000
		[Tooltip("Fungus message to listen for")]
		[SerializeField]
		protected string message = "";
	}
}
