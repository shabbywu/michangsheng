using System;
using System.Collections.Generic;
using System.Reflection;
using KBEngine;

namespace GUIPackage
{
	// Token: 0x02000D54 RID: 3412
	public class Equips : StaticSkill
	{
		// Token: 0x06005137 RID: 20791 RVA: 0x0003A7C5 File Offset: 0x000389C5
		public Equips(int id, int level, int max)
		{
			this.skill_ID = id;
			this.skill_level = level;
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x0003A7DB File Offset: 0x000389DB
		public override Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
		{
			return attaker.EquipSeidFlag;
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0021D724 File Offset: 0x0021B924
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

		// Token: 0x0600513A RID: 20794 RVA: 0x0003A7E3 File Offset: 0x000389E3
		public override JSONObject getJsonData()
		{
			return jsonData.instance._ItemJsonData;
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x0021D7A8 File Offset: 0x0021B9A8
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

		// Token: 0x0600513C RID: 20796 RVA: 0x0003A7EF File Offset: 0x000389EF
		public override string getMethodName()
		{
			return "realizeEquipSeid";
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x0021D8E4 File Offset: 0x0021BAE4
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

		// Token: 0x0600513E RID: 20798 RVA: 0x0003A7F6 File Offset: 0x000389F6
		public void realizeEquipSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.realizeSeid3(seid, damage, attaker, receiver, type);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x0003A805 File Offset: 0x00038A05
		public void realizeEquipSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			this.resetSeidFlag(seid, attaker);
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x0003A80F File Offset: 0x00038A0F
		public void realizeEquipSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
		{
			base.realizeSeid8(seid, damage, attaker, receiver, type);
		}

		// Token: 0x04005238 RID: 21048
		public JSONObject ItemAddSeid;

		// Token: 0x02000D55 RID: 3413
		public enum EquipSeidAll
		{
			// Token: 0x0400523A RID: 21050
			EquiSEID4 = 4,
			// Token: 0x0400523B RID: 21051
			EquiSEID3,
			// Token: 0x0400523C RID: 21052
			EquiSEID5 = 5,
			// Token: 0x0400523D RID: 21053
			EquiSEID6,
			// Token: 0x0400523E RID: 21054
			EquiSEID7
		}
	}
}
