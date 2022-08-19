using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F22 RID: 3874
	[CommandInfo("YS", "ItemCheck", "检测是否拥有某个物品", 0)]
	[AddComponentMenu("")]
	public class ItemCheck : Command
	{
		// Token: 0x06006DCF RID: 28111 RVA: 0x000E111A File Offset: 0x000DF31A
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x002A3EAC File Offset: 0x002A20AC
		public override void OnEnter()
		{
			int itemNum = Tools.instance.getPlayer().getItemNum(this.ItemID);
			bool value = false;
			if (this.CompareType == ItemCheck.CompareNum.GreaterThan)
			{
				if (itemNum > this.ItemNum)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.LessThan)
			{
				if (itemNum < this.ItemNum)
				{
					value = true;
				}
			}
			else if (this.CompareType == ItemCheck.CompareNum.equalTo && itemNum == this.ItemNum)
			{
				value = true;
			}
			this.TempBool.Value = value;
			this.Continue();
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B57 RID: 23383
		[Tooltip("需要检测物品ID")]
		[SerializeField]
		protected int ItemID;

		// Token: 0x04005B58 RID: 23384
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04005B59 RID: 23385
		[Tooltip("数量")]
		[SerializeField]
		protected int ItemNum;

		// Token: 0x04005B5A RID: 23386
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;

		// Token: 0x02001725 RID: 5925
		public enum CompareNum
		{
			// Token: 0x0400751C RID: 29980
			GreaterThan,
			// Token: 0x0400751D RID: 29981
			LessThan,
			// Token: 0x0400751E RID: 29982
			equalTo
		}
	}
}
