using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001402 RID: 5122
	[CommandInfo("YSNew/Get", "GetNpcId", "根据流派和境界获取NpcId", 0)]
	[AddComponentMenu("")]
	public class GetNpcId : Command
	{
		// Token: 0x06007C61 RID: 31841 RVA: 0x002C4C78 File Offset: 0x002C2E78
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
						if (!this.IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[text]["id"].I))
						{
							list.Add(avatarJsonData[text]["id"].I);
						}
					}
					else if (this.SetType == (SexType)avatarJsonData[text]["SexType"].I && (!this.IsNoFriend || !Tools.instance.getPlayer().emailDateMag.IsFriend(avatarJsonData[text]["id"].I)))
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

		// Token: 0x06007C62 RID: 31842 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A71 RID: 27249
		[Tooltip("Npc流派")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcLiuPai;

		// Token: 0x04006A72 RID: 27250
		[Tooltip("Npc境界")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcLevel;

		// Token: 0x04006A73 RID: 27251
		[Tooltip("NpcId存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NpcId;

		// Token: 0x04006A74 RID: 27252
		[SerializeField]
		protected bool IsNoFriend;

		// Token: 0x04006A75 RID: 27253
		[SerializeField]
		protected SexType SetType;
	}
}
