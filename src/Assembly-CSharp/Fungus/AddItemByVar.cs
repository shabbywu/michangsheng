using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E3 RID: 5091
	[CommandInfo("YSNew/Add", "AddItemByVar", "增加物品", 0)]
	[AddComponentMenu("")]
	public class AddItemByVar : Command
	{
		// Token: 0x06007BE7 RID: 31719 RVA: 0x002C42CC File Offset: 0x002C24CC
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			int num = this.ItemNum;
			if (this.ItemNumVar != null)
			{
				num = this.ItemNumVar.Value;
			}
			if (num > 0)
			{
				player.addItem(this.ItemID.Value, num, Tools.CreateItemSeid(this.ItemID.Value), true);
			}
			else
			{
				player.removeItem(this.ItemID.Value, Mathf.Abs(num));
			}
			this.Continue();
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A3E RID: 27198
		[Tooltip("增加物品的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04006A3F RID: 27199
		[Tooltip("增加物品的数量")]
		[SerializeField]
		protected int ItemNum;

		// Token: 0x04006A40 RID: 27200
		[Tooltip("增加物品的数量(如果为Null则使用数字)")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemNumVar;
	}
}
