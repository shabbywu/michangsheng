using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D8 RID: 5080
	[CommandInfo("YS", "ItemCheckVar", "检测是否拥有某个物品", 0)]
	[AddComponentMenu("")]
	public class ItemCheckVar : Command
	{
		// Token: 0x06007BBF RID: 31679 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x002C41E0 File Offset: 0x002C23E0
		public override void OnEnter()
		{
			int itemNum = Tools.instance.getPlayer().getItemNum(this.ItemID.Value);
			bool value = false;
			if (this.CompareType == ItemCheckVar.CompareNum.GreaterThan)
			{
				if (itemNum > this.ItemNum.Value)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheckVar.CompareNum.LessThan)
			{
				if (itemNum < this.ItemNum.Value)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheckVar.CompareNum.equalTo && itemNum == this.ItemNum.Value)
			{
				value = true;
			}
			this.TempBool.Value = value;
			this.Continue();
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A29 RID: 27177
		[Tooltip("需要检测物品ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04006A2A RID: 27178
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheckVar.CompareNum CompareType;

		// Token: 0x04006A2B RID: 27179
		[Tooltip("数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemNum;

		// Token: 0x04006A2C RID: 27180
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;

		// Token: 0x020013D9 RID: 5081
		public enum CompareNum
		{
			// Token: 0x04006A2E RID: 27182
			GreaterThan,
			// Token: 0x04006A2F RID: 27183
			LessThan,
			// Token: 0x04006A30 RID: 27184
			equalTo
		}
	}
}
