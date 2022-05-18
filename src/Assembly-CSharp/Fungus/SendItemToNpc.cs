using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200144F RID: 5199
	[CommandInfo("YSTools", "SendItemToNpc", "玩家给npc物品", 0)]
	[AddComponentMenu("")]
	public class SendItemToNpc : Command
	{
		// Token: 0x06007D88 RID: 32136 RVA: 0x002C68CC File Offset: 0x002C4ACC
		public override void OnEnter()
		{
			if (NpcJieSuanManager.inst.IsDeath(this.npcId.Value))
			{
				this.Continue();
				return;
			}
			List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
			int i = 0;
			while (i < values.Count)
			{
				if (values[i] != null && values[i].itemId == this.WeiXieItem.Value)
				{
					NpcJieSuanManager.inst.AddItemToNpcBackpack(this.npcId.Value, values[i].itemId, 1, values[i].Seid, false);
					if (values[i].itemCount == 1U)
					{
						values.Remove(values[i]);
						break;
					}
					values[i].itemCount -= 1U;
					break;
				}
				else
				{
					i++;
				}
			}
			this.Continue();
		}

		// Token: 0x06007D89 RID: 32137 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AEF RID: 27375
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AF0 RID: 27376
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WeiXieItem;
	}
}
