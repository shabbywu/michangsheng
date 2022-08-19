using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1D RID: 3869
	[CommandInfo("YS", "CheckShaQi", "检测杀气数量", 0)]
	[AddComponentMenu("")]
	public class CheckShaQi : Command
	{
		// Token: 0x06006DB9 RID: 28089 RVA: 0x002A3CD0 File Offset: 0x002A1ED0
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.shaQi;
			this.Continue();
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B4D RID: 23373
		[Tooltip("获取到的杀气值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
