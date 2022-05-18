using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011F3 RID: 4595
	[CommandInfo("", "Comment", "Use comments to record design notes and reminders about your game.", 0)]
	[AddComponentMenu("")]
	public class Comment : Command
	{
		// Token: 0x0600707C RID: 28796 RVA: 0x00011424 File Offset: 0x0000F624
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x0600707D RID: 28797 RVA: 0x0004C69C File Offset: 0x0004A89C
		public override string GetSummary()
		{
			if (this.commenterName != "")
			{
				return this.commenterName + ": " + this.commentText;
			}
			return this.commentText;
		}

		// Token: 0x0600707E RID: 28798 RVA: 0x0004C6CD File Offset: 0x0004A8CD
		public override Color GetButtonColor()
		{
			return new Color32(220, 220, 220, byte.MaxValue);
		}

		// Token: 0x0400630C RID: 25356
		[Tooltip("Name of Commenter")]
		[SerializeField]
		protected string commenterName = "";

		// Token: 0x0400630D RID: 25357
		[Tooltip("Text to display for this comment")]
		[TextArea(2, 4)]
		[SerializeField]
		protected string commentText = "";
	}
}
