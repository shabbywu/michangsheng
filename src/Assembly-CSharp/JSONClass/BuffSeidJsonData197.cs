using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B39 RID: 2873
	public class BuffSeidJsonData197 : IJSONClass
	{
		// Token: 0x0600484C RID: 18508 RVA: 0x001ECF08 File Offset: 0x001EB108
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[197].list)
			{
				try
				{
					BuffSeidJsonData197 buffSeidJsonData = new BuffSeidJsonData197();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData197.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData197.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData197.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData197.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData197.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData197.OnInitFinishAction != null)
			{
				BuffSeidJsonData197.OnInitFinishAction();
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004207 RID: 16903
		public static int SEIDID = 197;

		// Token: 0x04004208 RID: 16904
		public static Dictionary<int, BuffSeidJsonData197> DataDict = new Dictionary<int, BuffSeidJsonData197>();

		// Token: 0x04004209 RID: 16905
		public static List<BuffSeidJsonData197> DataList = new List<BuffSeidJsonData197>();

		// Token: 0x0400420A RID: 16906
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData197.OnInitFinish);

		// Token: 0x0400420B RID: 16907
		public int id;

		// Token: 0x0400420C RID: 16908
		public int target;

		// Token: 0x0400420D RID: 16909
		public int value1;
	}
}
