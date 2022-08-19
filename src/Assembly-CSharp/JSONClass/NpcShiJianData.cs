using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AB RID: 2219
	public class NpcShiJianData : IJSONClass
	{
		// Token: 0x060040BF RID: 16575 RVA: 0x001BAAA4 File Offset: 0x001B8CA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcShiJianData.list)
			{
				try
				{
					NpcShiJianData npcShiJianData = new NpcShiJianData();
					npcShiJianData.id = jsonobject["id"].I;
					npcShiJianData.ShiJianType = jsonobject["ShiJianType"].Str;
					npcShiJianData.ShiJianInfo = jsonobject["ShiJianInfo"].Str;
					if (NpcShiJianData.DataDict.ContainsKey(npcShiJianData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcShiJianData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcShiJianData.id));
					}
					else
					{
						NpcShiJianData.DataDict.Add(npcShiJianData.id, npcShiJianData);
						NpcShiJianData.DataList.Add(npcShiJianData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcShiJianData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcShiJianData.OnInitFinishAction != null)
			{
				NpcShiJianData.OnInitFinishAction();
			}
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EDE RID: 16094
		public static Dictionary<int, NpcShiJianData> DataDict = new Dictionary<int, NpcShiJianData>();

		// Token: 0x04003EDF RID: 16095
		public static List<NpcShiJianData> DataList = new List<NpcShiJianData>();

		// Token: 0x04003EE0 RID: 16096
		public static Action OnInitFinishAction = new Action(NpcShiJianData.OnInitFinish);

		// Token: 0x04003EE1 RID: 16097
		public int id;

		// Token: 0x04003EE2 RID: 16098
		public string ShiJianType;

		// Token: 0x04003EE3 RID: 16099
		public string ShiJianInfo;
	}
}
