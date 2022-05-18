using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C39 RID: 3129
	public class NpcShiJianData : IJSONClass
	{
		// Token: 0x06004C4D RID: 19533 RVA: 0x00203988 File Offset: 0x00201B88
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

		// Token: 0x06004C4E RID: 19534 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A36 RID: 18998
		public static Dictionary<int, NpcShiJianData> DataDict = new Dictionary<int, NpcShiJianData>();

		// Token: 0x04004A37 RID: 18999
		public static List<NpcShiJianData> DataList = new List<NpcShiJianData>();

		// Token: 0x04004A38 RID: 19000
		public static Action OnInitFinishAction = new Action(NpcShiJianData.OnInitFinish);

		// Token: 0x04004A39 RID: 19001
		public int id;

		// Token: 0x04004A3A RID: 19002
		public string ShiJianType;

		// Token: 0x04004A3B RID: 19003
		public string ShiJianInfo;
	}
}
