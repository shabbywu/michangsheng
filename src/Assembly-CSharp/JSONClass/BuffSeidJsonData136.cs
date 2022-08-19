using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000772 RID: 1906
	public class BuffSeidJsonData136 : IJSONClass
	{
		// Token: 0x06003BDC RID: 15324 RVA: 0x0019BBF0 File Offset: 0x00199DF0
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

		// Token: 0x06003BDD RID: 15325 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003532 RID: 13618
		public static int SEIDID = 136;

		// Token: 0x04003533 RID: 13619
		public static Dictionary<int, BuffSeidJsonData136> DataDict = new Dictionary<int, BuffSeidJsonData136>();

		// Token: 0x04003534 RID: 13620
		public static List<BuffSeidJsonData136> DataList = new List<BuffSeidJsonData136>();

		// Token: 0x04003535 RID: 13621
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData136.OnInitFinish);

		// Token: 0x04003536 RID: 13622
		public int id;

		// Token: 0x04003537 RID: 13623
		public int value1;

		// Token: 0x04003538 RID: 13624
		public List<int> value2 = new List<int>();
	}
}
