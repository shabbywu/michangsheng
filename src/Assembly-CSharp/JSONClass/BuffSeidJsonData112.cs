using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000761 RID: 1889
	public class BuffSeidJsonData112 : IJSONClass
	{
		// Token: 0x06003B98 RID: 15256 RVA: 0x0019A330 File Offset: 0x00198530
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[112].list)
			{
				try
				{
					BuffSeidJsonData112 buffSeidJsonData = new BuffSeidJsonData112();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData112.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData112.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData112.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData112.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData112.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData112.OnInitFinishAction != null)
			{
				BuffSeidJsonData112.OnInitFinishAction();
			}
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034BD RID: 13501
		public static int SEIDID = 112;

		// Token: 0x040034BE RID: 13502
		public static Dictionary<int, BuffSeidJsonData112> DataDict = new Dictionary<int, BuffSeidJsonData112>();

		// Token: 0x040034BF RID: 13503
		public static List<BuffSeidJsonData112> DataList = new List<BuffSeidJsonData112>();

		// Token: 0x040034C0 RID: 13504
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData112.OnInitFinish);

		// Token: 0x040034C1 RID: 13505
		public int id;

		// Token: 0x040034C2 RID: 13506
		public int value1;
	}
}
