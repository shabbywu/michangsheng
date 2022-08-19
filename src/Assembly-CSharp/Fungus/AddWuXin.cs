using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F38 RID: 3896
	[CommandInfo("YSNew/Add", "AddWuXin", "增加悟性", 0)]
	[AddComponentMenu("")]
	public class AddWuXin : Command
	{
		// Token: 0x06006E23 RID: 28195 RVA: 0x002A45BE File Offset: 0x002A27BE
		public override void OnEnter()
		{
			Tools.instance.getPlayer().addWuXin(this.AddWuXinNum);
			this.Continue();
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B84 RID: 23428
		[Tooltip("增加悟性的数量")]
		[SerializeField]
		protected int AddWuXinNum;
	}
}
