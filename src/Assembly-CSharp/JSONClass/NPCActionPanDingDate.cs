using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C23 RID: 3107
	public class NPCActionPanDingDate : IJSONClass
	{
		// Token: 0x06004BF5 RID: 19445 RVA: 0x00200FAC File Offset: 0x001FF1AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCActionPanDingDate.list)
			{
				try
				{
					NPCActionPanDingDate npcactionPanDingDate = new NPCActionPanDingDate();
					npcactionPanDingDate.id = jsonobject["id"].I;
					npcactionPanDingDate.ChangeTo = jsonobject["ChangeTo"].I;
					npcactionPanDingDate.PingJing = jsonobject["PingJing"].I;
					npcactionPanDingDate.LingShi = jsonobject["LingShi"].I;
					npcactionPanDingDate.BeiBao = jsonobject["BeiBao"].I;
					npcactionPanDingDate.PaiMaiTime = jsonobject["PaiMaiTime"].I;
					npcactionPanDingDate.PaiMaiType = jsonobject["PaiMaiType"].I;
					npcactionPanDingDate.JingJie = jsonobject["JingJie"].ToList();
					npcactionPanDingDate.YueFen = jsonobject["YueFen"].ToList();
					npcactionPanDingDate.LingHeDianWei = jsonobject["LingHeDianWei"].ToList();
					if (NPCActionPanDingDate.DataDict.ContainsKey(npcactionPanDingDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCActionPanDingDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcactionPanDingDate.id));
					}
					else
					{
						NPCActionPanDingDate.DataDict.Add(npcactionPanDingDate.id, npcactionPanDingDate);
						NPCActionPanDingDate.DataList.Add(npcactionPanDingDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCActionPanDingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCActionPanDingDate.OnInitFinishAction != null)
			{
				NPCActionPanDingDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004929 RID: 18729
		public static Dictionary<int, NPCActionPanDingDate> DataDict = new Dictionary<int, NPCActionPanDingDate>();

		// Token: 0x0400492A RID: 18730
		public static List<NPCActionPanDingDate> DataList = new List<NPCActionPanDingDate>();

		// Token: 0x0400492B RID: 18731
		public static Action OnInitFinishAction = new Action(NPCActionPanDingDate.OnInitFinish);

		// Token: 0x0400492C RID: 18732
		public int id;

		// Token: 0x0400492D RID: 18733
		public int ChangeTo;

		// Token: 0x0400492E RID: 18734
		public int PingJing;

		// Token: 0x0400492F RID: 18735
		public int LingShi;

		// Token: 0x04004930 RID: 18736
		public int BeiBao;

		// Token: 0x04004931 RID: 18737
		public int PaiMaiTime;

		// Token: 0x04004932 RID: 18738
		public int PaiMaiType;

		// Token: 0x04004933 RID: 18739
		public List<int> JingJie = new List<int>();

		// Token: 0x04004934 RID: 18740
		public List<int> YueFen = new List<int>();

		// Token: 0x04004935 RID: 18741
		public List<int> LingHeDianWei = new List<int>();
	}
}
