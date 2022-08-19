using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F2E RID: 3886
	[CommandInfo("YSNew/Add", "AddItemToNpc", "给npc背包添加物品", 0)]
	[AddComponentMenu("")]
	public class AddItemToNpc : Command
	{
		// Token: 0x06006DFF RID: 28159 RVA: 0x002A41C4 File Offset: 0x002A23C4
		public override void OnEnter()
		{
			int num = this.npcId.Value;
			if (this.isImprotant)
			{
				num = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[this.npcId.Value];
			}
			NpcJieSuanManager.inst.AddItemToNpcBackpack(num, this.item.Value, this.itemCount.Value, null, false);
			this.Continue();
		}

		// Token: 0x06006E00 RID: 28160 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005B6F RID: 23407
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005B70 RID: 23408
		[Tooltip("物品Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable item;

		// Token: 0x04005B71 RID: 23409
		[Tooltip("物品数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable itemCount;

		// Token: 0x04005B72 RID: 23410
		[Tooltip("是否是重要NPC")]
		[SerializeField]
		public bool isImprotant;
	}
}
