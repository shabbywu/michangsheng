using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200140E RID: 5134
	[CommandInfo("YSNew/Set", "CloseTieJianHongDian", "关闭铁剑红点", 0)]
	[AddComponentMenu("")]
	public class CloseTieJianHongDian : Command
	{
		// Token: 0x06007C93 RID: 31891 RVA: 0x002C52A4 File Offset: 0x002C34A4
		public override void OnEnter()
		{
			try
			{
				Tools.instance.getPlayer().TieJianHongDianList.RemoveField(this.HongDianID.ToString());
			}
			catch (Exception)
			{
			}
			this.Continue();
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A8D RID: 27277
		[Tooltip("红点ID")]
		[SerializeField]
		protected int HongDianID;
	}
}
