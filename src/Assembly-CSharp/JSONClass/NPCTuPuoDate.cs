using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B3 RID: 2227
	public class NPCTuPuoDate : IJSONClass
	{
		// Token: 0x060040DF RID: 16607 RVA: 0x001BBF9C File Offset: 0x001BA19C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCTuPuoDate.list)
			{
				try
				{
					NPCTuPuoDate npctuPuoDate = new NPCTuPuoDate();
					npctuPuoDate.id = jsonobject["id"].I;
					npctuPuoDate.jilv = jsonobject["jilv"].I;
					npctuPuoDate.mubiaojilv = jsonobject["mubiaojilv"].I;
					npctuPuoDate.shengyushouyuan = jsonobject["shengyushouyuan"].I;
					npctuPuoDate.ZiZhiJiaCheng = jsonobject["ZiZhiJiaCheng"].I;
					npctuPuoDate.LingShiPanDuan = jsonobject["LingShiPanDuan"].I;
					npctuPuoDate.sunshi = jsonobject["sunshi"].I;
					npctuPuoDate.FailAddLv = jsonobject["FailAddLv"].I;
					npctuPuoDate.JinDanFen = jsonobject["JinDanFen"].ToList();
					npctuPuoDate.ShouJiItem = jsonobject["ShouJiItem"].ToList();
					npctuPuoDate.TuPoItem = jsonobject["TuPoItem"].ToList();
					npctuPuoDate.TiShengJiLv = jsonobject["TiShengJiLv"].ToList();
					npctuPuoDate.ShangXian = jsonobject["ShangXian"].ToList();
					npctuPuoDate.TiShengJinDan = jsonobject["TiShengJinDan"].ToList();
					if (NPCTuPuoDate.DataDict.ContainsKey(npctuPuoDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCTuPuoDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npctuPuoDate.id));
					}
					else
					{
						NPCTuPuoDate.DataDict.Add(npctuPuoDate.id, npctuPuoDate);
						NPCTuPuoDate.DataList.Add(npctuPuoDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCTuPuoDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCTuPuoDate.OnInitFinishAction != null)
			{
				NPCTuPuoDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F6E RID: 16238
		public static Dictionary<int, NPCTuPuoDate> DataDict = new Dictionary<int, NPCTuPuoDate>();

		// Token: 0x04003F6F RID: 16239
		public static List<NPCTuPuoDate> DataList = new List<NPCTuPuoDate>();

		// Token: 0x04003F70 RID: 16240
		public static Action OnInitFinishAction = new Action(NPCTuPuoDate.OnInitFinish);

		// Token: 0x04003F71 RID: 16241
		public int id;

		// Token: 0x04003F72 RID: 16242
		public int jilv;

		// Token: 0x04003F73 RID: 16243
		public int mubiaojilv;

		// Token: 0x04003F74 RID: 16244
		public int shengyushouyuan;

		// Token: 0x04003F75 RID: 16245
		public int ZiZhiJiaCheng;

		// Token: 0x04003F76 RID: 16246
		public int LingShiPanDuan;

		// Token: 0x04003F77 RID: 16247
		public int sunshi;

		// Token: 0x04003F78 RID: 16248
		public int FailAddLv;

		// Token: 0x04003F79 RID: 16249
		public List<int> JinDanFen = new List<int>();

		// Token: 0x04003F7A RID: 16250
		public List<int> ShouJiItem = new List<int>();

		// Token: 0x04003F7B RID: 16251
		public List<int> TuPoItem = new List<int>();

		// Token: 0x04003F7C RID: 16252
		public List<int> TiShengJiLv = new List<int>();

		// Token: 0x04003F7D RID: 16253
		public List<int> ShangXian = new List<int>();

		// Token: 0x04003F7E RID: 16254
		public List<int> TiShengJinDan = new List<int>();
	}
}
