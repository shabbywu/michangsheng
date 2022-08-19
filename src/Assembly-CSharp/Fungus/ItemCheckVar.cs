using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F23 RID: 3875
	[CommandInfo("YS", "ItemCheckVar", "检测是否拥有某个物品", 0)]
	[AddComponentMenu("")]
	public class ItemCheckVar : Command
	{
		// Token: 0x06006DD4 RID: 28116 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x002A3F24 File Offset: 0x002A2124
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

		// Token: 0x06006DD6 RID: 28118 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B5B RID: 23387
		[Tooltip("需要检测物品ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04005B5C RID: 23388
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheckVar.CompareNum CompareType;

		// Token: 0x04005B5D RID: 23389
		[Tooltip("数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemNum;

		// Token: 0x04005B5E RID: 23390
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;

		// Token: 0x02001726 RID: 5926
		public enum CompareNum
		{
			// Token: 0x04007520 RID: 29984
			GreaterThan,
			// Token: 0x04007521 RID: 29985
			LessThan,
			// Token: 0x04007522 RID: 29986
			equalTo
		}
	}
}
