using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F59 RID: 3929
	[CommandInfo("YSNew/Set", "OpenTieJianHongDian", "显示铁剑红点", 0)]
	[AddComponentMenu("")]
	public class OpenTieJianHongDian : Command
	{
		// Token: 0x06006EA7 RID: 28327 RVA: 0x002A53EC File Offset: 0x002A35EC
		public override void OnEnter()
		{
			Tools.instance.getPlayer().TieJianHongDianList.SetField(this.HongDianID.ToString(), this.HongDianID.ToString());
			this.Continue();
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BBC RID: 23484
		[Tooltip("红点ID")]
		[SerializeField]
		protected int HongDianID;
	}
}
