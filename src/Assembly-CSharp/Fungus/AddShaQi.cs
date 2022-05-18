using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E8 RID: 5096
	[CommandInfo("YSNew/Add", "AddShaQi", "增加煞气", 0)]
	[AddComponentMenu("")]
	public class AddShaQi : Command
	{
		// Token: 0x06007BF7 RID: 31735 RVA: 0x0005453B File Offset: 0x0005273B
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShaQi(this.AddShaQiNum);
			this.Continue();
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A4B RID: 27211
		[Tooltip("增加煞气的数量")]
		[SerializeField]
		protected int AddShaQiNum;
	}
}
