using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200127E RID: 4734
	[CommandInfo("Flow", "Send Message", "Sends a message to either the owner Flowchart or all Flowcharts in the scene. Blocks can listen for this message using a Message Received event handler.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SendMessage : Command
	{
		// Token: 0x060072CB RID: 29387 RVA: 0x002A94E0 File Offset: 0x002A76E0
		public override void OnEnter()
		{
			if (this._message.Value.Length == 0)
			{
				this.Continue();
				return;
			}
			MessageReceived[] array;
			if (this.messageTarget == MessageTarget.SameFlowchart)
			{
				array = base.GetComponents<MessageReceived>();
			}
			else
			{
				array = Object.FindObjectsOfType<MessageReceived>();
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnSendFungusMessage(this._message.Value);
				}
			}
			this.Continue();
		}

		// Token: 0x060072CC RID: 29388 RVA: 0x0004E291 File Offset: 0x0004C491
		public override string GetSummary()
		{
			if (this._message.Value.Length == 0)
			{
				return "Error: No message specified";
			}
			return this._message.Value;
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x0004E2B6 File Offset: 0x0004C4B6
		public override bool HasReference(Variable variable)
		{
			return this._message.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		protected virtual void OnEnable()
		{
			if (this.messageOLD != "")
			{
				this._message.Value = this.messageOLD;
				this.messageOLD = "";
			}
		}

		// Token: 0x040064F2 RID: 25842
		[Tooltip("Target flowchart(s) to send the message to")]
		[SerializeField]
		protected MessageTarget messageTarget;

		// Token: 0x040064F3 RID: 25843
		[Tooltip("Name of the message to send")]
		[SerializeField]
		protected StringData _message = new StringData("");

		// Token: 0x040064F4 RID: 25844
		[HideInInspector]
		[FormerlySerializedAs("message")]
		public string messageOLD = "";
	}
}
