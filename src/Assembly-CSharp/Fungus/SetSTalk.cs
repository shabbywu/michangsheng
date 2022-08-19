using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F63 RID: 3939
	[CommandInfo("YSNew/Set", "SetTalk", "设置后续对话", 0)]
	[AddComponentMenu("")]
	public class SetSTalk : Command
	{
		// Token: 0x06006EC9 RID: 28361 RVA: 0x002A5772 File Offset: 0x002A3972
		public override void OnEnter()
		{
			GlobalValue.SetTalk(0, this.TalkID, base.GetCommandSourceDesc());
			this.Continue();
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BCA RID: 23498
		[Tooltip("后续对话的ID")]
		[SerializeField]
		protected int TalkID;
	}
}
