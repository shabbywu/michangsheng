using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B03 RID: 2819
	public class BuffSeidJsonData127 : IJSONClass
	{
		// Token: 0x06004776 RID: 18294 RVA: 0x001E8CA4 File Offset: 0x001E6EA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[127].list)
			{
				try
				{
					BuffSeidJsonData127 buffSeidJsonData = new BuffSeidJsonData127();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData127.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData127.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData127.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData127.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData127.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData127.OnInitFinishAction != null)
			{
				BuffSeidJsonData127.OnInitFinishAction();
			}
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004096 RID: 16534
		public static int SEIDID = 127;

		// Token: 0x04004097 RID: 16535
		public static Dictionary<int, BuffSeidJsonData127> DataDict = new Dictionary<int, BuffSeidJsonData127>();

		// Token: 0x04004098 RID: 16536
		public static List<BuffSeidJsonData127> DataList = new List<BuffSeidJsonData127>();

		// Token: 0x04004099 RID: 16537
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData127.OnInitFinish);

		// Token: 0x0400409A RID: 16538
		public int id;

		// Token: 0x0400409B RID: 16539
		public List<int> value1 = new List<int>();

		// Token: 0x0400409C RID: 16540
		public List<int> value2 = new List<int>();
	}
}
