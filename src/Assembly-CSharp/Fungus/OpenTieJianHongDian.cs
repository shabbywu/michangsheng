using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140F RID: 5135
	[CommandInfo("YSNew/Set", "OpenTieJianHongDian", "显示铁剑红点", 0)]
	[AddComponentMenu("")]
	public class OpenTieJianHongDian : Command
	{
		// Token: 0x06007C97 RID: 31895 RVA: 0x00054752 File Offset: 0x00052952
		public override void OnEnter()
		{
			Tools.instance.getPlayer().TieJianHongDianList.SetField(this.HongDianID.ToString(), this.HongDianID.ToString());
			this.Continue();
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A8E RID: 27278
		[Tooltip("红点ID")]
		[SerializeField]
		protected int HongDianID;
	}
}
