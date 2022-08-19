using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DBD RID: 3517
	[CommandInfo("", "Comment", "Use comments to record design notes and reminders about your game.", 0)]
	[AddComponentMenu("")]
	public class Comment : Command
	{
		// Token: 0x0600640F RID: 25615 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		public override void OnEnter()
		{
			this.Continue();
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0027D526 File Offset: 0x0027B726
		public override string GetSummary()
		{
			if (this.commenterName != "")
			{
				return this.commenterName + ": " + this.commentText;
			}
			return this.commentText;
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x0027D557 File Offset: 0x0027B757
		public override Color GetButtonColor()
		{
			return new Color32(220, 220, 220, byte.MaxValue);
		}

		// Token: 0x04005617 RID: 22039
		[Tooltip("Name of Commenter")]
		[SerializeField]
		protected string commenterName = "";

		// Token: 0x04005618 RID: 22040
		[Tooltip("Text to display for this comment")]
		[TextArea(2, 4)]
		[SerializeField]
		protected string commentText = "";
	}
}
