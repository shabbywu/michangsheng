using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200077B RID: 1915
	public class BuffSeidJsonData147 : IJSONClass
	{
		// Token: 0x06003C00 RID: 15360 RVA: 0x0019C938 File Offset: 0x0019AB38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[147].list)
			{
				try
				{
					BuffSeidJsonData147 buffSeidJsonData = new BuffSeidJsonData147();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData147.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData147.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData147.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData147.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData147.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData147.OnInitFinishAction != null)
			{
				BuffSeidJsonData147.OnInitFinishAction();
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003572 RID: 13682
		public static int SEIDID = 147;

		// Token: 0x04003573 RID: 13683
		public static Dictionary<int, BuffSeidJsonData147> DataDict = new Dictionary<int, BuffSeidJsonData147>();

		// Token: 0x04003574 RID: 13684
		public static List<BuffSeidJsonData147> DataList = new List<BuffSeidJsonData147>();

		// Token: 0x04003575 RID: 13685
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData147.OnInitFinish);

		// Token: 0x04003576 RID: 13686
		public int id;

		// Token: 0x04003577 RID: 13687
		public int value1;
	}
}
