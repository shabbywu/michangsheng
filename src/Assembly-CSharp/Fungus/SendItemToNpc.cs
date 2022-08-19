using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F9F RID: 3999
	[CommandInfo("YSTools", "SendItemToNpc", "玩家给npc物品", 0)]
	[AddComponentMenu("")]
	public class SendItemToNpc : Command
	{
		// Token: 0x06006FA8 RID: 28584 RVA: 0x002A73AC File Offset: 0x002A55AC
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

		// Token: 0x06006FA9 RID: 28585 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005C20 RID: 23584
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005C21 RID: 23585
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WeiXieItem;
	}
}
