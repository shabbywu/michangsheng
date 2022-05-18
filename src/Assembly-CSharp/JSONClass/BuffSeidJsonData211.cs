using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B48 RID: 2888
	public class BuffSeidJsonData211 : IJSONClass
	{
		// Token: 0x06004888 RID: 18568 RVA: 0x001EE0F8 File Offset: 0x001EC2F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[211].list)
			{
				try
				{
					BuffSeidJsonData211 buffSeidJsonData = new BuffSeidJsonData211();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData211.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData211.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData211.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData211.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData211.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData211.OnInitFinishAction != null)
			{
				BuffSeidJsonData211.OnInitFinishAction();
			}
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004266 RID: 16998
		public static int SEIDID = 211;

		// Token: 0x04004267 RID: 16999
		public static Dictionary<int, BuffSeidJsonData211> DataDict = new Dictionary<int, BuffSeidJsonData211>();

		// Token: 0x04004268 RID: 17000
		public static List<BuffSeidJsonData211> DataList = new List<BuffSeidJsonData211>();

		// Token: 0x04004269 RID: 17001
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData211.OnInitFinish);

		// Token: 0x0400426A RID: 17002
		public int id;

		// Token: 0x0400426B RID: 17003
		public int value1;
	}
}
