using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008B4 RID: 2228
	public class NpcWuDaoChiData : IJSONClass
	{
		// Token: 0x060040E3 RID: 16611 RVA: 0x001BC25C File Offset: 0x001BA45C
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

		// Token: 0x060040E4 RID: 16612 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F7F RID: 16255
		public static Dictionary<int, NpcWuDaoChiData> DataDict = new Dictionary<int, NpcWuDaoChiData>();

		// Token: 0x04003F80 RID: 16256
		public static List<NpcWuDaoChiData> DataList = new List<NpcWuDaoChiData>();

		// Token: 0x04003F81 RID: 16257
		public static Action OnInitFinishAction = new Action(NpcWuDaoChiData.OnInitFinish);

		// Token: 0x04003F82 RID: 16258
		public int id;

		// Token: 0x04003F83 RID: 16259
		public int xiaohao;

		// Token: 0x04003F84 RID: 16260
		public List<int> wudaochi = new List<int>();
	}
}
