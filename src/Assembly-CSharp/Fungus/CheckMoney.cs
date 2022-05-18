using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CF RID: 5071
	[CommandInfo("YS", "CheckMoney", "检测金钱数量", 0)]
	[AddComponentMenu("")]
	public class CheckMoney : Command
	{
		// Token: 0x06007B9C RID: 31644 RVA: 0x002C3F88 File Offset: 0x002C2188
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.money;
			this.Continue();
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A15 RID: 27157
		[Tooltip("获取到的金钱存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
