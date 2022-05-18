using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200141A RID: 5146
	[CommandInfo("YSNew/Set", "SetTalk", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetSTalk : Command
	{
		// Token: 0x06007CB9 RID: 31929 RVA: 0x0005483E File Offset: 0x00052A3E
		public override void OnEnter()
		{
			GlobalValue.SetTalk(0, this.TalkID, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A9F RID: 27295
		[Tooltip("后续对话的ID")]
		[SerializeField]
		protected int TalkID;
	}
}
