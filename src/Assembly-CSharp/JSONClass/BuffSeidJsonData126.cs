using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B02 RID: 2818
	public class BuffSeidJsonData126 : IJSONClass
	{
		// Token: 0x06004772 RID: 18290 RVA: 0x001E8B68 File Offset: 0x001E6D68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[126].list)
			{
				try
				{
					BuffSeidJsonData126 buffSeidJsonData = new BuffSeidJsonData126();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData126.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData126.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData126.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData126.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData126.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData126.OnInitFinishAction != null)
			{
				BuffSeidJsonData126.OnInitFinishAction();
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400408F RID: 16527
		public static int SEIDID = 126;

		// Token: 0x04004090 RID: 16528
		public static Dictionary<int, BuffSeidJsonData126> DataDict = new Dictionary<int, BuffSeidJsonData126>();

		// Token: 0x04004091 RID: 16529
		public static List<BuffSeidJsonData126> DataList = new List<BuffSeidJsonData126>();

		// Token: 0x04004092 RID: 16530
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData126.OnInitFinish);

		// Token: 0x04004093 RID: 16531
		public int id;

		// Token: 0x04004094 RID: 16532
		public int value1;

		// Token: 0x04004095 RID: 16533
		public int value2;
	}
}
