using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBD RID: 3005
	public class CyTeShuNpc : IJSONClass
	{
		// Token: 0x06004A5C RID: 19036 RVA: 0x001F7BC8 File Offset: 0x001F5DC8
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

		// Token: 0x06004A5D RID: 19037 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045D8 RID: 17880
		public static Dictionary<int, CyTeShuNpc> DataDict = new Dictionary<int, CyTeShuNpc>();

		// Token: 0x040045D9 RID: 17881
		public static List<CyTeShuNpc> DataList = new List<CyTeShuNpc>();

		// Token: 0x040045DA RID: 17882
		public static Action OnInitFinishAction = new Action(CyTeShuNpc.OnInitFinish);

		// Token: 0x040045DB RID: 17883
		public int id;

		// Token: 0x040045DC RID: 17884
		public int Type;

		// Token: 0x040045DD RID: 17885
		public int PaiMaiID;
	}
}
