using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C31 RID: 3121
	public class NpcImprotantPanDingData : IJSONClass
	{
		// Token: 0x06004C2D RID: 19501 RVA: 0x002028D8 File Offset: 0x00200AD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcImprotantPanDingData.list)
			{
				try
				{
					NpcImprotantPanDingData npcImprotantPanDingData = new NpcImprotantPanDingData();
					npcImprotantPanDingData.id = jsonobject["id"].I;
					npcImprotantPanDingData.NPC = jsonobject["NPC"].I;
					npcImprotantPanDingData.XingWei = jsonobject["XingWei"].I;
					npcImprotantPanDingData.fuhao = jsonobject["fuhao"].Str;
					npcImprotantPanDingData.StartTime = jsonobject["StartTime"].Str;
					npcImprotantPanDingData.EndTime = jsonobject["EndTime"].Str;
					npcImprotantPanDingData.EventValue = jsonobject["EventValue"].ToList();
					if (NpcImprotantPanDingData.DataDict.ContainsKey(npcImprotantPanDingData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcImprotantPanDingData.id));
					}
					else
					{
						NpcImprotantPanDingData.DataDict.Add(npcImprotantPanDingData.id, npcImprotantPanDingData);
						NpcImprotantPanDingData.DataList.Add(npcImprotantPanDingData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcImprotantPanDingData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcImprotantPanDingData.OnInitFinishAction != null)
			{
				NpcImprotantPanDingData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049C4 RID: 18884
		public static Dictionary<int, NpcImprotantPanDingData> DataDict = new Dictionary<int, NpcImprotantPanDingData>();

		// Token: 0x040049C5 RID: 18885
		public static List<NpcImprotantPanDingData> DataList = new List<NpcImprotantPanDingData>();

		// Token: 0x040049C6 RID: 18886
		public static Action OnInitFinishAction = new Action(NpcImprotantPanDingData.OnInitFinish);

		// Token: 0x040049C7 RID: 18887
		public int id;

		// Token: 0x040049C8 RID: 18888
		public int NPC;

		// Token: 0x040049C9 RID: 18889
		public int XingWei;

		// Token: 0x040049CA RID: 18890
		public string fuhao;

		// Token: 0x040049CB RID: 18891
		public string StartTime;

		// Token: 0x040049CC RID: 18892
		public string EndTime;

		// Token: 0x040049CD RID: 18893
		public List<int> EventValue = new List<int>();
	}
}
