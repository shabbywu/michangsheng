using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2D RID: 3885
	[CommandInfo("YSNew/Add", "AddItemByVar", "增加物品", 0)]
	[AddComponentMenu("")]
	public class AddItemByVar : Command
	{
		// Token: 0x06006DFC RID: 28156 RVA: 0x002A4144 File Offset: 0x002A2344
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

		// Token: 0x06006DFD RID: 28157 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B6C RID: 23404
		[Tooltip("增加物品的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemID;

		// Token: 0x04005B6D RID: 23405
		[Tooltip("增加物品的数量")]
		[SerializeField]
		protected int ItemNum;

		// Token: 0x04005B6E RID: 23406
		[Tooltip("增加物品的数量(如果为Null则使用数字)")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable ItemNumVar;
	}
}
