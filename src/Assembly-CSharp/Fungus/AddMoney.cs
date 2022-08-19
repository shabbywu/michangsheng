using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F30 RID: 3888
	[CommandInfo("YSNew/Add", "AddMoney", "增加或减少金钱（减少的话直接填负数）", 0)]
	[AddComponentMenu("")]
	public class AddMoney : Command
	{
		// Token: 0x06006E05 RID: 28165 RVA: 0x002A4264 File Offset: 0x002A2464
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int num = (int)player.money;
			int num2 = num + this.AddNum;
			if (this.AddNum == 0)
			{
				num2 = num + this.AddSum.Value;
				if (this.AddSum.Value >= 0)
				{
					UIPopTip.Inst.Pop(Tools.getStr("AddMoney1").Replace("{X}", this.AddSum.Value.ToString()), PopTipIconType.上箭头);
				}
				else
				{
					UIPopTip.Inst.Pop(Tools.getStr("AddMoney2").Replace("{X}", (-this.AddSum.Value).ToString()), PopTipIconType.下箭头);
				}
			}
			else if (this.AddNum >= 0)
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney1").Replace("{X}", this.AddNum.ToString()), PopTipIconType.上箭头);
			}
			else
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney2").Replace("{X}", (-this.AddNum).ToString()), PopTipIconType.下箭头);
			}
			if (num2 >= 0)
			{
				player.money = (ulong)num2;
			}
			else
			{
				player.money = 0UL;
			}
			this.Continue();
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B74 RID: 23412
		[Tooltip("增加金钱的数量")]
		[SerializeField]
		public int AddNum;

		// Token: 0x04005B75 RID: 23413
		[Tooltip("增加金钱的数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AddSum;
	}
}
