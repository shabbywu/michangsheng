using System;
using System.Collections.Generic;
using System.Reflection;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000A4D RID: 2637
	public class Equips : StaticSkill
	{
		// Token: 0x06004872 RID: 18546 RVA: 0x001E9973 File Offset: 0x001E7B73
		public Equips(int id, int level, int max)
		{
			this.skill_ID = id;
			this.skill_level = level;
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x001E9989 File Offset: 0x001E7B89
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.EquipSeidFlag;
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x001E9994 File Offset: 0x001E7B94
		public override void realizeSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
		{
			Avatar avatar = (Avatar)_attaker;
			Avatar avatar2 = (Avatar)_receiver;
			int i = 0;
			while (i < 500)
			{
				if (i == seid)
				{
					MethodInfo method = base.GetType().GetMethod(this.getMethodName() + seid);
					if (method != null)
					{
						method.Invoke(this, new object[]
						{
							seid,
							flag,
							avatar,
							avatar2,
							type
						});
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x001E9A18 File Offset: 0x001E7C18
		public override JSONObject getJsonData()
		{
			return jsonData.instance._ItemJsonData;
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x001E9A24 File Offset: 0x001E7C24
		public static void resetEquipSeid(Avatar attaker)
		{
			attaker.EquipSeidFlag.Clear();
			for (int i = attaker.equipItemList.values.Count - 1; i >= 0; i--)
			{
				if (attaker.equipItemList.values[i] == null)
				{
					attaker.equipItemList.values.RemoveAt(i);
				}
			}
			foreach (ITEM_INFO item_INFO in attaker.equipItemList.values)
			{
				if (item_INFO.Seid != null && item_INFO.Seid.HasField("ItemSeids"))
				{
					using (List<JSONObject>.Enumerator enumerator2 = item_INFO.Seid["ItemSeids"].list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							JSONObject itemAddSeid = enumerator2.Current;
							new Equips(item_INFO.itemId, 0, 5)
							{
								ItemAddSeid = itemAddSeid
							}.Puting(attaker, attaker, 2);
						}
						continue;
					}
				}
				new Equips(item_INFO.itemId, 0, 5).Puting(attaker, attaker, 2);
			}
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x001E9B60 File Offset: 0x001E7D60
		public override string getMethodName()
		{
			return "realizeEquipSeid";
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x001E9B68 File Offset: 0x001E7D68
		public override JSONObject getSeidJson(int seid)
		{
			if (this.ItemAddSeid != null)
			{
				foreach (JSONObject jsonobject in this.ItemAddSeid.list)
				{
					if (jsonobject["id"].I == seid)
					{
						return jsonobject;
					}
				}
			}
			return jsonData.instance.EquipSeidJsonData[seid][string.Concat(this.skill_ID)];
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x001E9BFC File Offset: 0x001E7DFC
		public void realizeEquipSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.realizeSeid3(seid, damage, attaker, receiver, type);
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x001E9C0B File Offset: 0x001E7E0B
		public void realizeEquipSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x001E9C15 File Offset: 0x001E7E15
		public void realizeEquipSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			base.realizeSeid8(seid, damage, attaker, receiver, type);
		}

		// Token: 0x040048FA RID: 18682
		public JSONObject ItemAddSeid;

		// Token: 0x0200157B RID: 5499
		public enum EquipSeidAll
		{
			// Token: 0x04006F84 RID: 28548
			EquiSEID4 = 4,
			// Token: 0x04006F85 RID: 28549
			EquiSEID3,
			// Token: 0x04006F86 RID: 28550
			EquiSEID5 = 5,
			// Token: 0x04006F87 RID: 28551
			EquiSEID6,
			// Token: 0x04006F88 RID: 28552
			EquiSEID7
		}
	}
}
