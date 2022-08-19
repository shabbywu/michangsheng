using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B5 RID: 2229
	public class NPCWuDaoJson : IJSONClass
	{
		// Token: 0x060040E7 RID: 16615 RVA: 0x001BC3D4 File Offset: 0x001BA5D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCWuDaoJson.list)
			{
				try
				{
					NPCWuDaoJson npcwuDaoJson = new NPCWuDaoJson();
					npcwuDaoJson.id = jsonobject["id"].I;
					npcwuDaoJson.Type = jsonobject["Type"].I;
					npcwuDaoJson.lv = jsonobject["lv"].I;
					npcwuDaoJson.value1 = jsonobject["value1"].I;
					npcwuDaoJson.value2 = jsonobject["value2"].I;
					npcwuDaoJson.value3 = jsonobject["value3"].I;
					npcwuDaoJson.value4 = jsonobject["value4"].I;
					npcwuDaoJson.value5 = jsonobject["value5"].I;
					npcwuDaoJson.value6 = jsonobject["value6"].I;
					npcwuDaoJson.value7 = jsonobject["value7"].I;
					npcwuDaoJson.value8 = jsonobject["value8"].I;
					npcwuDaoJson.value9 = jsonobject["value9"].I;
					npcwuDaoJson.value10 = jsonobject["value10"].I;
					npcwuDaoJson.value11 = jsonobject["value11"].I;
					npcwuDaoJson.value12 = jsonobject["value12"].I;
					npcwuDaoJson.wudaoID = jsonobject["wudaoID"].ToList();
					if (NPCWuDaoJson.DataDict.ContainsKey(npcwuDaoJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcwuDaoJson.id));
					}
					else
					{
						NPCWuDaoJson.DataDict.Add(npcwuDaoJson.id, npcwuDaoJson);
						NPCWuDaoJson.DataList.Add(npcwuDaoJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCWuDaoJson.OnInitFinishAction != null)
			{
				NPCWuDaoJson.OnInitFinishAction();
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F85 RID: 16261
		public static Dictionary<int, NPCWuDaoJson> DataDict = new Dictionary<int, NPCWuDaoJson>();

		// Token: 0x04003F86 RID: 16262
		public static List<NPCWuDaoJson> DataList = new List<NPCWuDaoJson>();

		// Token: 0x04003F87 RID: 16263
		public static Action OnInitFinishAction = new Action(NPCWuDaoJson.OnInitFinish);

		// Token: 0x04003F88 RID: 16264
		public int id;

		// Token: 0x04003F89 RID: 16265
		public int Type;

		// Token: 0x04003F8A RID: 16266
		public int lv;

		// Token: 0x04003F8B RID: 16267
		public int value1;

		// Token: 0x04003F8C RID: 16268
		public int value2;

		// Token: 0x04003F8D RID: 16269
		public int value3;

		// Token: 0x04003F8E RID: 16270
		public int value4;

		// Token: 0x04003F8F RID: 16271
		public int value5;

		// Token: 0x04003F90 RID: 16272
		public int value6;

		// Token: 0x04003F91 RID: 16273
		public int value7;

		// Token: 0x04003F92 RID: 16274
		public int value8;

		// Token: 0x04003F93 RID: 16275
		public int value9;

		// Token: 0x04003F94 RID: 16276
		public int value10;

		// Token: 0x04003F95 RID: 16277
		public int value11;

		// Token: 0x04003F96 RID: 16278
		public int value12;

		// Token: 0x04003F97 RID: 16279
		public List<int> wudaoID = new List<int>();
	}
}
