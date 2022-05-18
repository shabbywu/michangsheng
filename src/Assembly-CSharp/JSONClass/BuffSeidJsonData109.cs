using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF5 RID: 2805
	public class BuffSeidJsonData109 : IJSONClass
	{
		// Token: 0x0600473E RID: 18238 RVA: 0x001E7C20 File Offset: 0x001E5E20
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[109].list)
			{
				try
				{
					BuffSeidJsonData109 buffSeidJsonData = new BuffSeidJsonData109();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData109.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData109.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData109.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData109.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData109.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData109.OnInitFinishAction != null)
			{
				BuffSeidJsonData109.OnInitFinishAction();
			}
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400403E RID: 16446
		public static int SEIDID = 109;

		// Token: 0x0400403F RID: 16447
		public static Dictionary<int, BuffSeidJsonData109> DataDict = new Dictionary<int, BuffSeidJsonData109>();

		// Token: 0x04004040 RID: 16448
		public static List<BuffSeidJsonData109> DataList = new List<BuffSeidJsonData109>();

		// Token: 0x04004041 RID: 16449
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData109.OnInitFinish);

		// Token: 0x04004042 RID: 16450
		public int id;

		// Token: 0x04004043 RID: 16451
		public List<int> value1 = new List<int>();
	}
}
