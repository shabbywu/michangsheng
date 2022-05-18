using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C41 RID: 3137
	public class NPCTuPuoDate : IJSONClass
	{
		// Token: 0x06004C6D RID: 19565 RVA: 0x00204CB8 File Offset: 0x00202EB8
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

		// Token: 0x06004C6E RID: 19566 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AC2 RID: 19138
		public static Dictionary<int, NPCTuPuoDate> DataDict = new Dictionary<int, NPCTuPuoDate>();

		// Token: 0x04004AC3 RID: 19139
		public static List<NPCTuPuoDate> DataList = new List<NPCTuPuoDate>();

		// Token: 0x04004AC4 RID: 19140
		public static Action OnInitFinishAction = new Action(NPCTuPuoDate.OnInitFinish);

		// Token: 0x04004AC5 RID: 19141
		public int id;

		// Token: 0x04004AC6 RID: 19142
		public int jilv;

		// Token: 0x04004AC7 RID: 19143
		public int mubiaojilv;

		// Token: 0x04004AC8 RID: 19144
		public int shengyushouyuan;

		// Token: 0x04004AC9 RID: 19145
		public int ZiZhiJiaCheng;

		// Token: 0x04004ACA RID: 19146
		public int LingShiPanDuan;

		// Token: 0x04004ACB RID: 19147
		public int sunshi;

		// Token: 0x04004ACC RID: 19148
		public int FailAddLv;

		// Token: 0x04004ACD RID: 19149
		public List<int> JinDanFen = new List<int>();

		// Token: 0x04004ACE RID: 19150
		public List<int> ShouJiItem = new List<int>();

		// Token: 0x04004ACF RID: 19151
		public List<int> TuPoItem = new List<int>();

		// Token: 0x04004AD0 RID: 19152
		public List<int> TiShengJiLv = new List<int>();

		// Token: 0x04004AD1 RID: 19153
		public List<int> ShangXian = new List<int>();

		// Token: 0x04004AD2 RID: 19154
		public List<int> TiShengJinDan = new List<int>();
	}
}
