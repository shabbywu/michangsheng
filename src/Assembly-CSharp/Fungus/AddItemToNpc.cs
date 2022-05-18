using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013E4 RID: 5092
	[CommandInfo("YSNew/Add", "AddItemToNpc", "给npc背包添加物品", 0)]
	[AddComponentMenu("")]
	public class AddItemToNpc : Command
	{
		// Token: 0x06007BEA RID: 31722 RVA: 0x002C434C File Offset: 0x002C254C
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

		// Token: 0x06007BEB RID: 31723 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006A41 RID: 27201
		[Tooltip("NpcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006A42 RID: 27202
		[Tooltip("物品Id")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable item;

		// Token: 0x04006A43 RID: 27203
		[Tooltip("物品数量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable itemCount;

		// Token: 0x04006A44 RID: 27204
		[Tooltip("是否是重要NPC")]
		[SerializeField]
		public bool isImprotant;
	}
}
