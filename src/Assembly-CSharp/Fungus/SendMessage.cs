using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E2D RID: 3629
	[CommandInfo("Flow", "Send Message", "Sends a message to either the owner Flowchart or all Flowcharts in the scene. Blocks can listen for this message using a Message Received event handler.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class SendMessage : Command
	{
		// Token: 0x0600663D RID: 26173 RVA: 0x00285C64 File Offset: 0x00283E64
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

		// Token: 0x0600663E RID: 26174 RVA: 0x00285CCD File Offset: 0x00283ECD
		public override string GetSummary()
		{
			if (this._message.Value.Length == 0)
			{
				return "Error: No message specified";
			}
			return this._message.Value;
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x00285CF2 File Offset: 0x00283EF2
		public override bool HasReference(Variable variable)
		{
			return this._message.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x00285D10 File Offset: 0x00283F10
		protected virtual void OnEnable()
		{
			if (this.messageOLD != "")
			{
				this._message.Value = this.messageOLD;
				this.messageOLD = "";
			}
		}

		// Token: 0x040057AE RID: 22446
		[Tooltip("Target flowchart(s) to send the message to")]
		[SerializeField]
		protected MessageTarget messageTarget;

		// Token: 0x040057AF RID: 22447
		[Tooltip("Name of the message to send")]
		[SerializeField]
		protected StringData _message = new StringData("");

		// Token: 0x040057B0 RID: 22448
		[HideInInspector]
		[FormerlySerializedAs("message")]
		public string messageOLD = "";
	}
}
