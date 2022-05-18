using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B42 RID: 2882
	public class BuffSeidJsonData205 : IJSONClass
	{
		// Token: 0x06004870 RID: 18544 RVA: 0x001ED9C8 File Offset: 0x001EBBC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[205].list)
			{
				try
				{
					BuffSeidJsonData205 buffSeidJsonData = new BuffSeidJsonData205();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData205.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData205.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData205.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData205.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData205.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData205.OnInitFinishAction != null)
			{
				BuffSeidJsonData205.OnInitFinishAction();
			}
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004240 RID: 16960
		public static int SEIDID = 205;

		// Token: 0x04004241 RID: 16961
		public static Dictionary<int, BuffSeidJsonData205> DataDict = new Dictionary<int, BuffSeidJsonData205>();

		// Token: 0x04004242 RID: 16962
		public static List<BuffSeidJsonData205> DataList = new List<BuffSeidJsonData205>();

		// Token: 0x04004243 RID: 16963
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData205.OnInitFinish);

		// Token: 0x04004244 RID: 16964
		public int id;

		// Token: 0x04004245 RID: 16965
		public int value1;
	}
}
