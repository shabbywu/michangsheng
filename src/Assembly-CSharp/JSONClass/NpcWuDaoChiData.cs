using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C42 RID: 3138
	public class NpcWuDaoChiData : IJSONClass
	{
		// Token: 0x06004C71 RID: 19569 RVA: 0x00204F54 File Offset: 0x00203154
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcWuDaoChiData.list)
			{
				try
				{
					NpcWuDaoChiData npcWuDaoChiData = new NpcWuDaoChiData();
					npcWuDaoChiData.id = jsonobject["id"].I;
					npcWuDaoChiData.xiaohao = jsonobject["xiaohao"].I;
					npcWuDaoChiData.wudaochi = jsonobject["wudaochi"].ToList();
					if (NpcWuDaoChiData.DataDict.ContainsKey(npcWuDaoChiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcWuDaoChiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcWuDaoChiData.id));
					}
					else
					{
						NpcWuDaoChiData.DataDict.Add(npcWuDaoChiData.id, npcWuDaoChiData);
						NpcWuDaoChiData.DataList.Add(npcWuDaoChiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcWuDaoChiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcWuDaoChiData.OnInitFinishAction != null)
			{
				NpcWuDaoChiData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AD3 RID: 19155
		public static Dictionary<int, NpcWuDaoChiData> DataDict = new Dictionary<int, NpcWuDaoChiData>();

		// Token: 0x04004AD4 RID: 19156
		public static List<NpcWuDaoChiData> DataList = new List<NpcWuDaoChiData>();

		// Token: 0x04004AD5 RID: 19157
		public static Action OnInitFinishAction = new Action(NpcWuDaoChiData.OnInitFinish);

		// Token: 0x04004AD6 RID: 19158
		public int id;

		// Token: 0x04004AD7 RID: 19159
		public int xiaohao;

		// Token: 0x04004AD8 RID: 19160
		public List<int> wudaochi = new List<int>();
	}
}
