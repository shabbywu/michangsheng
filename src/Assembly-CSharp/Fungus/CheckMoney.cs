using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F1B RID: 3867
	[CommandInfo("YS", "CheckMoney", "检测金钱数量", 0)]
	[AddComponentMenu("")]
	public class CheckMoney : Command
	{
		// Token: 0x06006DB1 RID: 28081 RVA: 0x002A3C34 File Offset: 0x002A1E34
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			this.TempValue.Value = (int)player.money;
			this.Continue();
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B4B RID: 23371
		[Tooltip("获取到的金钱存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable TempValue;
	}
}
