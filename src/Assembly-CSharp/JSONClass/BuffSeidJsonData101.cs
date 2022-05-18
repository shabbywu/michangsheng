using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF0 RID: 2800
	public class BuffSeidJsonData101 : IJSONClass
	{
		// Token: 0x0600472A RID: 18218 RVA: 0x001E7630 File Offset: 0x001E5830
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[101].list)
			{
				try
				{
					BuffSeidJsonData101 buffSeidJsonData = new BuffSeidJsonData101();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData101.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData101.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData101.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData101.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData101.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData101.OnInitFinishAction != null)
			{
				BuffSeidJsonData101.OnInitFinishAction();
			}
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400401E RID: 16414
		public static int SEIDID = 101;

		// Token: 0x0400401F RID: 16415
		public static Dictionary<int, BuffSeidJsonData101> DataDict = new Dictionary<int, BuffSeidJsonData101>();

		// Token: 0x04004020 RID: 16416
		public static List<BuffSeidJsonData101> DataList = new List<BuffSeidJsonData101>();

		// Token: 0x04004021 RID: 16417
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData101.OnInitFinish);

		// Token: 0x04004022 RID: 16418
		public int id;

		// Token: 0x04004023 RID: 16419
		public int value1;
	}
}
