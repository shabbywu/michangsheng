using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B27 RID: 2855
	public class BuffSeidJsonData171 : IJSONClass
	{
		// Token: 0x06004804 RID: 18436 RVA: 0x001EB8D0 File Offset: 0x001E9AD0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[171].list)
			{
				try
				{
					BuffSeidJsonData171 buffSeidJsonData = new BuffSeidJsonData171();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData171.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData171.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData171.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData171.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData171.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData171.OnInitFinishAction != null)
			{
				BuffSeidJsonData171.OnInitFinishAction();
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400418E RID: 16782
		public static int SEIDID = 171;

		// Token: 0x0400418F RID: 16783
		public static Dictionary<int, BuffSeidJsonData171> DataDict = new Dictionary<int, BuffSeidJsonData171>();

		// Token: 0x04004190 RID: 16784
		public static List<BuffSeidJsonData171> DataList = new List<BuffSeidJsonData171>();

		// Token: 0x04004191 RID: 16785
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData171.OnInitFinish);

		// Token: 0x04004192 RID: 16786
		public int id;

		// Token: 0x04004193 RID: 16787
		public int value1;

		// Token: 0x04004194 RID: 16788
		public int value2;

		// Token: 0x04004195 RID: 16789
		public int value3;
	}
}
