using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2A RID: 3882
	[CommandInfo("YSNew/Add", "AddHP", "增加生命值", 0)]
	[AddComponentMenu("")]
	public class AddHP : Command
	{
		// Token: 0x06006DF0 RID: 28144 RVA: 0x002A4099 File Offset: 0x002A2299
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AllMapAddHP(this.AddHpNum, DeathType.身死道消);
			this.Continue();
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B67 RID: 23399
		[Tooltip("增加生命的数量")]
		[SerializeField]
		public int AddHpNum;
	}
}
