using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF8 RID: 2808
	public class BuffSeidJsonData111 : IJSONClass
	{
		// Token: 0x0600474A RID: 18250 RVA: 0x001E7F98 File Offset: 0x001E6198
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[111].list)
			{
				try
				{
					BuffSeidJsonData111 buffSeidJsonData = new BuffSeidJsonData111();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData111.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData111.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData111.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData111.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData111.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData111.OnInitFinishAction != null)
			{
				BuffSeidJsonData111.OnInitFinishAction();
			}
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004050 RID: 16464
		public static int SEIDID = 111;

		// Token: 0x04004051 RID: 16465
		public static Dictionary<int, BuffSeidJsonData111> DataDict = new Dictionary<int, BuffSeidJsonData111>();

		// Token: 0x04004052 RID: 16466
		public static List<BuffSeidJsonData111> DataList = new List<BuffSeidJsonData111>();

		// Token: 0x04004053 RID: 16467
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData111.OnInitFinish);

		// Token: 0x04004054 RID: 16468
		public int id;

		// Token: 0x04004055 RID: 16469
		public List<int> value1 = new List<int>();
	}
}
