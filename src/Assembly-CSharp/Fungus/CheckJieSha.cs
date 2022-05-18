using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001433 RID: 5171
	[CommandInfo("YSTools", "CheckJieSha", "检查是否能截杀", 0)]
	[AddComponentMenu("")]
	public class CheckJieSha : Command
	{
		// Token: 0x06007D16 RID: 32022 RVA: 0x002C6178 File Offset: 0x002C4378
		public override void OnEnter()
		{
			int nowMapIndex = Tools.instance.getPlayer().NowMapIndex;
			this.JieSha.Value = false;
			if (NpcJieSuanManager.inst.isCanJieSuan)
			{
				List<int> jieShaNpcList = NpcJieSuanManager.inst.GetJieShaNpcList(nowMapIndex);
				List<ITEM_INFO> values = Tools.instance.getPlayer().itemList.values;
				if (jieShaNpcList.Count > 0)
				{
					using (List<int>.Enumerator enumerator = jieShaNpcList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int value = enumerator.Current;
							if (this.JieSha.Value)
							{
								break;
							}
							int i = jsonData.instance.AvatarJsonData[value.ToString()]["Level"].I;
							int i2 = jsonData.instance.NpcLevelShouYiDate[i.ToString()]["jieshapanduan"].I;
							foreach (ITEM_INFO item_INFO in values)
							{
								if (item_INFO != null && item_INFO.itemId > 0 && jsonData.instance.ItemJsonData[item_INFO.itemId.ToString()]["price"].I >= i2)
								{
									this.JieSha.Value = true;
									this.npcId.Value = value;
									UINPCJiaoHu.Inst.JiaoHuItemID = item_INFO.itemId;
									this.WeiXieItem.Value = item_INFO.itemId;
									break;
								}
							}
						}
						goto IL_1AF;
					}
				}
				this.JieSha.Value = false;
			}
			else
			{
				this.JieSha.Value = false;
			}
			IL_1AF:
			this.Continue();
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006AC3 RID: 27331
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04006AC4 RID: 27332
		[Tooltip("是否截杀")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable JieSha;

		// Token: 0x04006AC5 RID: 27333
		[Tooltip("威胁物品")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WeiXieItem;
	}
}
