using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008A0 RID: 2208
	public class NpcHaoGanDuData : IJSONClass
	{
		// Token: 0x06004093 RID: 16531 RVA: 0x001B91E4 File Offset: 0x001B73E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcHaoGanDuData.list)
			{
				try
				{
					NpcHaoGanDuData npcHaoGanDuData = new NpcHaoGanDuData();
					npcHaoGanDuData.id = jsonobject["id"].I;
					npcHaoGanDuData.XiShu = jsonobject["XiShu"].I;
					npcHaoGanDuData.HaoGanDu = jsonobject["HaoGanDu"].Str;
					npcHaoGanDuData.QuJian = jsonobject["QuJian"].ToList();
					if (NpcHaoGanDuData.DataDict.ContainsKey(npcHaoGanDuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcHaoGanDuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcHaoGanDuData.id));
					}
					else
					{
						NpcHaoGanDuData.DataDict.Add(npcHaoGanDuData.id, npcHaoGanDuData);
						NpcHaoGanDuData.DataList.Add(npcHaoGanDuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcHaoGanDuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcHaoGanDuData.OnInitFinishAction != null)
			{
				NpcHaoGanDuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003E47 RID: 15943
		public static Dictionary<int, NpcHaoGanDuData> DataDict = new Dictionary<int, NpcHaoGanDuData>();

		// Token: 0x04003E48 RID: 15944
		public static List<NpcHaoGanDuData> DataList = new List<NpcHaoGanDuData>();

		// Token: 0x04003E49 RID: 15945
		public static Action OnInitFinishAction = new Action(NpcHaoGanDuData.OnInitFinish);

		// Token: 0x04003E4A RID: 15946
		public int id;

		// Token: 0x04003E4B RID: 15947
		public int XiShu;

		// Token: 0x04003E4C RID: 15948
		public string HaoGanDu;

		// Token: 0x04003E4D RID: 15949
		public List<int> QuJian = new List<int>();
	}
}
