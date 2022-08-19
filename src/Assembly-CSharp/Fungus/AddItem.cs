using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2C RID: 3884
	[CommandInfo("YSNew/Add", "AddItem", "增加物品", 0)]
	[AddComponentMenu("")]
	public class AddItem : Command
	{
		// Token: 0x06006DF8 RID: 28152 RVA: 0x002A40D4 File Offset: 0x002A22D4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			if (this.ItemNum > 0)
			{
				player.addItem(this.ItemID, this.ItemNum, Tools.CreateItemSeid(this.ItemID), this.showText);
			}
			else
			{
				player.removeItem(this.ItemID, this.ItemNum);
			}
			this.Continue();
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B69 RID: 23401
		[Tooltip("增加物品的ID")]
		[SerializeField]
		public int ItemID;

		// Token: 0x04005B6A RID: 23402
		[Tooltip("增加物品的数量")]
		[SerializeField]
		public int ItemNum;

		// Token: 0x04005B6B RID: 23403
		[Tooltip("是否显示增加物品弹框")]
		[SerializeField]
		public bool showText = true;
	}
}
