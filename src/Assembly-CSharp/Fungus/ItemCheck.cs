using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013D6 RID: 5078
	[CommandInfo("YS", "ItemCheck", "检测是否拥有某个物品", 0)]
	[AddComponentMenu("")]
	public class ItemCheck : Command
	{
		// Token: 0x06007BBA RID: 31674 RVA: 0x0001CA4F File Offset: 0x0001AC4F
		public void setHasVariable(string name, int num, Flowchart flowchart)
		{
			if (flowchart.HasVariable(name))
			{
				flowchart.SetIntegerVariable(name, num);
			}
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x002C4168 File Offset: 0x002C2368
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

		// Token: 0x06007BBC RID: 31676 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A21 RID: 27169
		[Tooltip("需要检测物品ID")]
		[SerializeField]
		protected int ItemID;

		// Token: 0x04006A22 RID: 27170
		[Tooltip("比较类型，大于 小于 等于")]
		[SerializeField]
		protected ItemCheck.CompareNum CompareType;

		// Token: 0x04006A23 RID: 27171
		[Tooltip("数量")]
		[SerializeField]
		protected int ItemNum;

		// Token: 0x04006A24 RID: 27172
		[Tooltip("将检测到的值赋给一个变量")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempBool;

		// Token: 0x020013D7 RID: 5079
		public enum CompareNum
		{
			// Token: 0x04006A26 RID: 27174
			GreaterThan,
			// Token: 0x04006A27 RID: 27175
			LessThan,
			// Token: 0x04006A28 RID: 27176
			equalTo
		}
	}
}
