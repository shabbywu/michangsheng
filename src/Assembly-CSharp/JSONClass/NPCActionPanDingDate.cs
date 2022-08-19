using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000895 RID: 2197
	public class NPCActionPanDingDate : IJSONClass
	{
		// Token: 0x06004067 RID: 16487 RVA: 0x001B7BC8 File Offset: 0x001B5DC8
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

		// Token: 0x06004068 RID: 16488 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DD0 RID: 15824
		public static Dictionary<int, NPCActionPanDingDate> DataDict = new Dictionary<int, NPCActionPanDingDate>();

		// Token: 0x04003DD1 RID: 15825
		public static List<NPCActionPanDingDate> DataList = new List<NPCActionPanDingDate>();

		// Token: 0x04003DD2 RID: 15826
		public static Action OnInitFinishAction = new Action(NPCActionPanDingDate.OnInitFinish);

		// Token: 0x04003DD3 RID: 15827
		public int id;

		// Token: 0x04003DD4 RID: 15828
		public int ChangeTo;

		// Token: 0x04003DD5 RID: 15829
		public int PingJing;

		// Token: 0x04003DD6 RID: 15830
		public int LingShi;

		// Token: 0x04003DD7 RID: 15831
		public int BeiBao;

		// Token: 0x04003DD8 RID: 15832
		public int PaiMaiTime;

		// Token: 0x04003DD9 RID: 15833
		public int PaiMaiType;

		// Token: 0x04003DDA RID: 15834
		public List<int> JingJie = new List<int>();

		// Token: 0x04003DDB RID: 15835
		public List<int> YueFen = new List<int>();

		// Token: 0x04003DDC RID: 15836
		public List<int> LingHeDianWei = new List<int>();
	}
}
