using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000827 RID: 2087
	public class CyTeShuNpc : IJSONClass
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x001AC95C File Offset: 0x001AAB5C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyTeShuNpc.list)
			{
				try
				{
					CyTeShuNpc cyTeShuNpc = new CyTeShuNpc();
					cyTeShuNpc.id = jsonobject["id"].I;
					cyTeShuNpc.Type = jsonobject["Type"].I;
					cyTeShuNpc.PaiMaiID = jsonobject["PaiMaiID"].I;
					if (CyTeShuNpc.DataDict.ContainsKey(cyTeShuNpc.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyTeShuNpc.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyTeShuNpc.id));
					}
					else
					{
						CyTeShuNpc.DataDict.Add(cyTeShuNpc.id, cyTeShuNpc);
						CyTeShuNpc.DataList.Add(cyTeShuNpc);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyTeShuNpc.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyTeShuNpc.OnInitFinishAction != null)
			{
				CyTeShuNpc.OnInitFinishAction();
			}
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A50 RID: 14928
		public static Dictionary<int, CyTeShuNpc> DataDict = new Dictionary<int, CyTeShuNpc>();

		// Token: 0x04003A51 RID: 14929
		public static List<CyTeShuNpc> DataList = new List<CyTeShuNpc>();

		// Token: 0x04003A52 RID: 14930
		public static Action OnInitFinishAction = new Action(CyTeShuNpc.OnInitFinish);

		// Token: 0x04003A53 RID: 14931
		public int id;

		// Token: 0x04003A54 RID: 14932
		public int Type;

		// Token: 0x04003A55 RID: 14933
		public int PaiMaiID;
	}
}
