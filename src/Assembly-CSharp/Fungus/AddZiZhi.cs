using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F3A RID: 3898
	[CommandInfo("YS", "AddZiZhi", "增加资质", 0)]
	[AddComponentMenu("")]
	public class AddZiZhi : Command
	{
		// Token: 0x06006E2A RID: 28202 RVA: 0x002A466F File Offset: 0x002A286F
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addZiZhi(this.AddZiZhiNum);
			this.Continue();
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B86 RID: 23430
		[Tooltip("增加资质的数量")]
		[SerializeField]
		protected int AddZiZhiNum;
	}
}
