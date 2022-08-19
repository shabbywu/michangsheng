using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000792 RID: 1938
	public class BuffSeidJsonData175 : IJSONClass
	{
		// Token: 0x06003C5A RID: 15450 RVA: 0x0019E930 File Offset: 0x0019CB30
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[175].list)
			{
				try
				{
					BuffSeidJsonData175 buffSeidJsonData = new BuffSeidJsonData175();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData175.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData175.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData175.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData175.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData175.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData175.OnInitFinishAction != null)
			{
				BuffSeidJsonData175.OnInitFinishAction();
			}
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003609 RID: 13833
		public static int SEIDID = 175;

		// Token: 0x0400360A RID: 13834
		public static Dictionary<int, BuffSeidJsonData175> DataDict = new Dictionary<int, BuffSeidJsonData175>();

		// Token: 0x0400360B RID: 13835
		public static List<BuffSeidJsonData175> DataList = new List<BuffSeidJsonData175>();

		// Token: 0x0400360C RID: 13836
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData175.OnInitFinish);

		// Token: 0x0400360D RID: 13837
		public int id;

		// Token: 0x0400360E RID: 13838
		public int value1;
	}
}
