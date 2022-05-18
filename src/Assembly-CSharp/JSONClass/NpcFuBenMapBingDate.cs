using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2C RID: 3116
	public class NpcFuBenMapBingDate : IJSONClass
	{
		// Token: 0x06004C19 RID: 19481 RVA: 0x00201F74 File Offset: 0x00200174
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcFuBenMapBingDate.list)
			{
				try
				{
					NpcFuBenMapBingDate npcFuBenMapBingDate = new NpcFuBenMapBingDate();
					npcFuBenMapBingDate.id = jsonobject["id"].I;
					npcFuBenMapBingDate.CaiJi = jsonobject["CaiJi"].I;
					npcFuBenMapBingDate.CaiKuang = jsonobject["CaiKuang"].I;
					npcFuBenMapBingDate.XunLuo = jsonobject["XunLuo"].I;
					npcFuBenMapBingDate.LingHe = jsonobject["LingHe"].I;
					npcFuBenMapBingDate.CaiJiDian = jsonobject["CaiJiDian"].ToList();
					npcFuBenMapBingDate.CaiKuangDian = jsonobject["CaiKuangDian"].ToList();
					npcFuBenMapBingDate.XunLuoDian = jsonobject["XunLuoDian"].ToList();
					npcFuBenMapBingDate.LingHeDian1 = jsonobject["LingHeDian1"].ToList();
					npcFuBenMapBingDate.LingHeDian2 = jsonobject["LingHeDian2"].ToList();
					npcFuBenMapBingDate.LingHeDian3 = jsonobject["LingHeDian3"].ToList();
					npcFuBenMapBingDate.LingHeDian4 = jsonobject["LingHeDian4"].ToList();
					npcFuBenMapBingDate.LingHeDian5 = jsonobject["LingHeDian5"].ToList();
					npcFuBenMapBingDate.LingHeDian6 = jsonobject["LingHeDian6"].ToList();
					if (NpcFuBenMapBingDate.DataDict.ContainsKey(npcFuBenMapBingDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcFuBenMapBingDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcFuBenMapBingDate.id));
					}
					else
					{
						NpcFuBenMapBingDate.DataDict.Add(npcFuBenMapBingDate.id, npcFuBenMapBingDate);
						NpcFuBenMapBingDate.DataList.Add(npcFuBenMapBingDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcFuBenMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcFuBenMapBingDate.OnInitFinishAction != null)
			{
				NpcFuBenMapBingDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004989 RID: 18825
		public static Dictionary<int, NpcFuBenMapBingDate> DataDict = new Dictionary<int, NpcFuBenMapBingDate>();

		// Token: 0x0400498A RID: 18826
		public static List<NpcFuBenMapBingDate> DataList = new List<NpcFuBenMapBingDate>();

		// Token: 0x0400498B RID: 18827
		public static Action OnInitFinishAction = new Action(NpcFuBenMapBingDate.OnInitFinish);

		// Token: 0x0400498C RID: 18828
		public int id;

		// Token: 0x0400498D RID: 18829
		public int CaiJi;

		// Token: 0x0400498E RID: 18830
		public int CaiKuang;

		// Token: 0x0400498F RID: 18831
		public int XunLuo;

		// Token: 0x04004990 RID: 18832
		public int LingHe;

		// Token: 0x04004991 RID: 18833
		public List<int> CaiJiDian = new List<int>();

		// Token: 0x04004992 RID: 18834
		public List<int> CaiKuangDian = new List<int>();

		// Token: 0x04004993 RID: 18835
		public List<int> XunLuoDian = new List<int>();

		// Token: 0x04004994 RID: 18836
		public List<int> LingHeDian1 = new List<int>();

		// Token: 0x04004995 RID: 18837
		public List<int> LingHeDian2 = new List<int>();

		// Token: 0x04004996 RID: 18838
		public List<int> LingHeDian3 = new List<int>();

		// Token: 0x04004997 RID: 18839
		public List<int> LingHeDian4 = new List<int>();

		// Token: 0x04004998 RID: 18840
		public List<int> LingHeDian5 = new List<int>();

		// Token: 0x04004999 RID: 18841
		public List<int> LingHeDian6 = new List<int>();
	}
}
