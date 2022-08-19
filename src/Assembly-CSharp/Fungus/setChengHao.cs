using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F5A RID: 3930
	[CommandInfo("YSNew/Set", "setChengHao", "设置称号id", 0)]
	[AddComponentMenu("")]
	public class setChengHao : Command
	{
		// Token: 0x06006EAB RID: 28331 RVA: 0x002A541E File Offset: 0x002A361E
		public override void OnEnter()
		{
			Tools.instance.getPlayer().SetChengHaoId(this.chengHaoID);
			this.Continue();
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BBD RID: 23485
		[Tooltip("设置称号的ID")]
		[SerializeField]
		protected int chengHaoID;
	}
}
