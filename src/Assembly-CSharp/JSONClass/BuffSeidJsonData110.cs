using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF7 RID: 2807
	public class BuffSeidJsonData110 : IJSONClass
	{
		// Token: 0x06004746 RID: 18246 RVA: 0x001E7E70 File Offset: 0x001E6070
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[110].list)
			{
				try
				{
					BuffSeidJsonData110 buffSeidJsonData = new BuffSeidJsonData110();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData110.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData110.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData110.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData110.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData110.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData110.OnInitFinishAction != null)
			{
				BuffSeidJsonData110.OnInitFinishAction();
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400404A RID: 16458
		public static int SEIDID = 110;

		// Token: 0x0400404B RID: 16459
		public static Dictionary<int, BuffSeidJsonData110> DataDict = new Dictionary<int, BuffSeidJsonData110>();

		// Token: 0x0400404C RID: 16460
		public static List<BuffSeidJsonData110> DataList = new List<BuffSeidJsonData110>();

		// Token: 0x0400404D RID: 16461
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData110.OnInitFinish);

		// Token: 0x0400404E RID: 16462
		public int id;

		// Token: 0x0400404F RID: 16463
		public List<int> value1 = new List<int>();
	}
}
