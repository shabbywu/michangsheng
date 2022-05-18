using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C2E RID: 3118
	public class NpcHaoGanDuData : IJSONClass
	{
		// Token: 0x06004C21 RID: 19489 RVA: 0x0020236C File Offset: 0x0020056C
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

		// Token: 0x06004C22 RID: 19490 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040049A0 RID: 18848
		public static Dictionary<int, NpcHaoGanDuData> DataDict = new Dictionary<int, NpcHaoGanDuData>();

		// Token: 0x040049A1 RID: 18849
		public static List<NpcHaoGanDuData> DataList = new List<NpcHaoGanDuData>();

		// Token: 0x040049A2 RID: 18850
		public static Action OnInitFinishAction = new Action(NpcHaoGanDuData.OnInitFinish);

		// Token: 0x040049A3 RID: 18851
		public int id;

		// Token: 0x040049A4 RID: 18852
		public int XiShu;

		// Token: 0x040049A5 RID: 18853
		public string HaoGanDu;

		// Token: 0x040049A6 RID: 18854
		public List<int> QuJian = new List<int>();
	}
}
