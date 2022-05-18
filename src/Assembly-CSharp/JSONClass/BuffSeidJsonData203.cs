using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B40 RID: 2880
	public class BuffSeidJsonData203 : IJSONClass
	{
		// Token: 0x06004868 RID: 18536 RVA: 0x001ED75C File Offset: 0x001EB95C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[203].list)
			{
				try
				{
					BuffSeidJsonData203 buffSeidJsonData = new BuffSeidJsonData203();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData203.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData203.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData203.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData203.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData203.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData203.OnInitFinishAction != null)
			{
				BuffSeidJsonData203.OnInitFinishAction();
			}
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004233 RID: 16947
		public static int SEIDID = 203;

		// Token: 0x04004234 RID: 16948
		public static Dictionary<int, BuffSeidJsonData203> DataDict = new Dictionary<int, BuffSeidJsonData203>();

		// Token: 0x04004235 RID: 16949
		public static List<BuffSeidJsonData203> DataList = new List<BuffSeidJsonData203>();

		// Token: 0x04004236 RID: 16950
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData203.OnInitFinish);

		// Token: 0x04004237 RID: 16951
		public int id;

		// Token: 0x04004238 RID: 16952
		public int value1;
	}
}
