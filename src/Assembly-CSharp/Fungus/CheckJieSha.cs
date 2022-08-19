using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F7C RID: 3964
	[CommandInfo("YSTools", "CheckJieSha", "检查是否能截杀", 0)]
	[AddComponentMenu("")]
	public class CheckJieSha : Command
	{
		// Token: 0x06006F26 RID: 28454 RVA: 0x002A6620 File Offset: 0x002A4820
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

		// Token: 0x06006F27 RID: 28455 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005BEF RID: 23535
		[Tooltip("npcId")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable npcId;

		// Token: 0x04005BF0 RID: 23536
		[Tooltip("是否截杀")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable JieSha;

		// Token: 0x04005BF1 RID: 23537
		[Tooltip("威胁物品")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable WeiXieItem;
	}
}
