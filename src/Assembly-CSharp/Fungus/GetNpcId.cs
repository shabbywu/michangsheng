using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F4C RID: 3916
	[CommandInfo("YSNew/Get", "GetNpcId", "根据流派和境界获取NpcId", 0)]
	[AddComponentMenu("")]
	public class GetNpcId : Command
	{
		// Token: 0x06006E76 RID: 28278 RVA: 0x002A4D18 File Offset: 0x002A2F18
		public override void OnEnter()
		{
			JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
			List<int> list = new List<int>();
			int value = this.NpcLiuPai.Value;
			int value2 = this.NpcLevel.Value;
			foreach (string text in avatarJsonData.keys)
			{
				if (int.Parse(text) >= 20000 && avatarJsonData[text]["LiuPai"].I == value && value2 == avatarJsonData[text]["Level"].I)
				{
					if (this.SetType == SexType.随机)
					{
						if ((!this.IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[text]["id"].I)) && (!this.IsNoImportantNpcId || !NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsValue(avatarJsonData[text]["id"].I)))
						{
							list.Add(avatarJsonData[text]["id"].I);
						}
					}
					else if (this.SetType == (SexType)avatarJsonData[text]["SexType"].I && (!this.IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[text]["id"].I)) && (!this.IsNoImportantNpcId || !NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsValue(avatarJsonData[text]["id"].I)))
					{
						list.Add(avatarJsonData[text]["id"].I);
					}
				}
			}
			if (list.Count < 1)
			{
				this.NpcId.Value = FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(value, value2, (int)this.SetType);
			}
			else
			{
				this.NpcId.Value = list[NpcJieSuanManager.inst.getRandomInt(0, list.Count - 1)];
			}
			this.Continue();
		}

		// Token: 0x06006E77 RID: 28279 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B9F RID: 23455
		[Tooltip("Npc流派")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcLiuPai;

		// Token: 0x04005BA0 RID: 23456
		[Tooltip("Npc境界")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcLevel;

		// Token: 0x04005BA1 RID: 23457
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;

		// Token: 0x04005BA2 RID: 23458
		[SerializeField]
		protected bool IsNoFriend;

		// Token: 0x04005BA3 RID: 23459
		[SerializeField]
		protected bool IsNoImportantNpcId;

		// Token: 0x04005BA4 RID: 23460
		[SerializeField]
		protected SexType SetType;
	}
}
