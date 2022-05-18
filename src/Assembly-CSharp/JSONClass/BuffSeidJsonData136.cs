using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0A RID: 2826
	public class BuffSeidJsonData136 : IJSONClass
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x001E95C0 File Offset: 0x001E77C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[136].list)
			{
				try
				{
					BuffSeidJsonData136 buffSeidJsonData = new BuffSeidJsonData136();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData136.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData136.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData136.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData136.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData136.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData136.OnInitFinishAction != null)
			{
				BuffSeidJsonData136.OnInitFinishAction();
			}
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040CB RID: 16587
		public static int SEIDID = 136;

		// Token: 0x040040CC RID: 16588
		public static Dictionary<int, BuffSeidJsonData136> DataDict = new Dictionary<int, BuffSeidJsonData136>();

		// Token: 0x040040CD RID: 16589
		public static List<BuffSeidJsonData136> DataList = new List<BuffSeidJsonData136>();

		// Token: 0x040040CE RID: 16590
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData136.OnInitFinish);

		// Token: 0x040040CF RID: 16591
		public int id;

		// Token: 0x040040D0 RID: 16592
		public int value1;

		// Token: 0x040040D1 RID: 16593
		public List<int> value2 = new List<int>();
	}
}
