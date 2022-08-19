using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089E RID: 2206
	public class NpcFuBenMapBingDate : IJSONClass
	{
		// Token: 0x0600408B RID: 16523 RVA: 0x001B8D9C File Offset: 0x001B6F9C
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

		// Token: 0x0600408C RID: 16524 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E30 RID: 15920
		public static Dictionary<int, NpcFuBenMapBingDate> DataDict = new Dictionary<int, NpcFuBenMapBingDate>();

		// Token: 0x04003E31 RID: 15921
		public static List<NpcFuBenMapBingDate> DataList = new List<NpcFuBenMapBingDate>();

		// Token: 0x04003E32 RID: 15922
		public static Action OnInitFinishAction = new Action(NpcFuBenMapBingDate.OnInitFinish);

		// Token: 0x04003E33 RID: 15923
		public int id;

		// Token: 0x04003E34 RID: 15924
		public int CaiJi;

		// Token: 0x04003E35 RID: 15925
		public int CaiKuang;

		// Token: 0x04003E36 RID: 15926
		public int XunLuo;

		// Token: 0x04003E37 RID: 15927
		public int LingHe;

		// Token: 0x04003E38 RID: 15928
		public List<int> CaiJiDian = new List<int>();

		// Token: 0x04003E39 RID: 15929
		public List<int> CaiKuangDian = new List<int>();

		// Token: 0x04003E3A RID: 15930
		public List<int> XunLuoDian = new List<int>();

		// Token: 0x04003E3B RID: 15931
		public List<int> LingHeDian1 = new List<int>();

		// Token: 0x04003E3C RID: 15932
		public List<int> LingHeDian2 = new List<int>();

		// Token: 0x04003E3D RID: 15933
		public List<int> LingHeDian3 = new List<int>();

		// Token: 0x04003E3E RID: 15934
		public List<int> LingHeDian4 = new List<int>();

		// Token: 0x04003E3F RID: 15935
		public List<int> LingHeDian5 = new List<int>();

		// Token: 0x04003E40 RID: 15936
		public List<int> LingHeDian6 = new List<int>();
	}
}
