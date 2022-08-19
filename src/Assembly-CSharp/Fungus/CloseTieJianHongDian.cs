using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F58 RID: 3928
	[CommandInfo("YSNew/Set", "CloseTieJianHongDian", "关闭铁剑红点", 0)]
	[AddComponentMenu("")]
	public class CloseTieJianHongDian : Command
	{
		// Token: 0x06006EA3 RID: 28323 RVA: 0x002A53A4 File Offset: 0x002A35A4
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

		// Token: 0x06006EA4 RID: 28324 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BBB RID: 23483
		[Tooltip("红点ID")]
		[SerializeField]
		protected int HongDianID;
	}
}
