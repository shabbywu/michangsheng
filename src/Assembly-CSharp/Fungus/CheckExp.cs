using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F18 RID: 3864
	[CommandInfo("YS", "CheckExp", "检测经验数量", 0)]
	[AddComponentMenu("")]
	public class CheckExp : Command
	{
		// Token: 0x06006DA5 RID: 28069 RVA: 0x002A3B20 File Offset: 0x002A1D20
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.exp;
			this.Continue();
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B45 RID: 23365
		[Tooltip("获取到的修为值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
