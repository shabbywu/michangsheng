using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2B RID: 3883
	[CommandInfo("YSNew/Add", "AddHPMax", "增加生命最大值", 0)]
	[AddComponentMenu("")]
	public class AddHPMax : Command
	{
		// Token: 0x06006DF4 RID: 28148 RVA: 0x002A40B7 File Offset: 0x002A22B7
		public override void OnEnter()
		{
			Tools.instance.getPlayer().AllMapAddHPMax(this.AddHPMaxNum);
			this.Continue();
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B68 RID: 23400
		[Tooltip("增加经验的数量")]
		[SerializeField]
		protected int AddHPMaxNum;
	}
}
