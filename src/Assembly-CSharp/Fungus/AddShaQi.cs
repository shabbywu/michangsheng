using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F32 RID: 3890
	[CommandInfo("YSNew/Add", "AddShaQi", "增加煞气", 0)]
	[AddComponentMenu("")]
	public class AddShaQi : Command
	{
		// Token: 0x06006E0C RID: 28172 RVA: 0x002A441B File Offset: 0x002A261B
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addShaQi(this.AddShaQiNum);
			this.Continue();
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B79 RID: 23417
		[Tooltip("增加煞气的数量")]
		[SerializeField]
		protected int AddShaQiNum;
	}
}
