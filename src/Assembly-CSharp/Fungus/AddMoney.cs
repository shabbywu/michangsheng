using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E6 RID: 5094
	[CommandInfo("YSNew/Add", "AddMoney", "增加或减少金钱（减少的话直接填负数）", 0)]
	[AddComponentMenu("")]
	public class AddMoney : Command
	{
		// Token: 0x06007BF0 RID: 31728 RVA: 0x002C43B4 File Offset: 0x002C25B4
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

		// Token: 0x06007BF1 RID: 31729 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A46 RID: 27206
		[Tooltip("增加金钱的数量")]
		[SerializeField]
		public int AddNum;

		// Token: 0x04006A47 RID: 27207
		[Tooltip("增加金钱的数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AddSum;
	}
}
