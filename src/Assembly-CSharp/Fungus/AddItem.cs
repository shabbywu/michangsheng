using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E2 RID: 5090
	[CommandInfo("YSNew/Add", "AddItem", "增加物品", 0)]
	[AddComponentMenu("")]
	public class AddItem : Command
	{
		// Token: 0x06007BE3 RID: 31715 RVA: 0x002C426C File Offset: 0x002C246C
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

		// Token: 0x06007BE4 RID: 31716 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A3B RID: 27195
		[Tooltip("增加物品的ID")]
		[SerializeField]
		public int ItemID;

		// Token: 0x04006A3C RID: 27196
		[Tooltip("增加物品的数量")]
		[SerializeField]
		public int ItemNum;

		// Token: 0x04006A3D RID: 27197
		[Tooltip("是否显示增加物品弹框")]
		[SerializeField]
		public bool showText = true;
	}
}
