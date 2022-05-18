using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2B RID: 3115
	public class NpcCreateData : IJSONClass
	{
		// Token: 0x06004C15 RID: 19477 RVA: 0x00201E24 File Offset: 0x00200024
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcCreateData.list)
			{
				try
				{
					NpcCreateData npcCreateData = new NpcCreateData();
					npcCreateData.id = jsonobject["id"].I;
					npcCreateData.NumA = jsonobject["NumA"].I;
					npcCreateData.NumB = jsonobject["NumB"].I;
					npcCreateData.EventValue = jsonobject["EventValue"].ToList();
					if (NpcCreateData.DataDict.ContainsKey(npcCreateData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcCreateData.id));
					}
					else
					{
						NpcCreateData.DataDict.Add(npcCreateData.id, npcCreateData);
						NpcCreateData.DataList.Add(npcCreateData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcCreateData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcCreateData.OnInitFinishAction != null)
			{
				NpcCreateData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004982 RID: 18818
		public static Dictionary<int, NpcCreateData> DataDict = new Dictionary<int, NpcCreateData>();

		// Token: 0x04004983 RID: 18819
		public static List<NpcCreateData> DataList = new List<NpcCreateData>();

		// Token: 0x04004984 RID: 18820
		public static Action OnInitFinishAction = new Action(NpcCreateData.OnInitFinish);

		// Token: 0x04004985 RID: 18821
		public int id;

		// Token: 0x04004986 RID: 18822
		public int NumA;

		// Token: 0x04004987 RID: 18823
		public int NumB;

		// Token: 0x04004988 RID: 18824
		public List<int> EventValue = new List<int>();
	}
}
