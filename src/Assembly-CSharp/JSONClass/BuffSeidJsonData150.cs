using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B17 RID: 2839
	public class BuffSeidJsonData150 : IJSONClass
	{
		// Token: 0x060047C6 RID: 18374 RVA: 0x001EA5E4 File Offset: 0x001E87E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[150].list)
			{
				try
				{
					BuffSeidJsonData150 buffSeidJsonData = new BuffSeidJsonData150();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData150.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData150.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData150.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData150.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData150.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData150.OnInitFinishAction != null)
			{
				BuffSeidJsonData150.OnInitFinishAction();
			}
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004124 RID: 16676
		public static int SEIDID = 150;

		// Token: 0x04004125 RID: 16677
		public static Dictionary<int, BuffSeidJsonData150> DataDict = new Dictionary<int, BuffSeidJsonData150>();

		// Token: 0x04004126 RID: 16678
		public static List<BuffSeidJsonData150> DataList = new List<BuffSeidJsonData150>();

		// Token: 0x04004127 RID: 16679
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData150.OnInitFinish);

		// Token: 0x04004128 RID: 16680
		public int id;

		// Token: 0x04004129 RID: 16681
		public int value1;
	}
}
