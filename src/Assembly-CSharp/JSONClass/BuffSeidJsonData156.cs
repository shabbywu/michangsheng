using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B18 RID: 2840
	public class BuffSeidJsonData156 : IJSONClass
	{
		// Token: 0x060047CA RID: 18378 RVA: 0x001EA710 File Offset: 0x001E8910
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[156].list)
			{
				try
				{
					BuffSeidJsonData156 buffSeidJsonData = new BuffSeidJsonData156();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData156.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData156.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData156.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData156.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData156.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData156.OnInitFinishAction != null)
			{
				BuffSeidJsonData156.OnInitFinishAction();
			}
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400412A RID: 16682
		public static int SEIDID = 156;

		// Token: 0x0400412B RID: 16683
		public static Dictionary<int, BuffSeidJsonData156> DataDict = new Dictionary<int, BuffSeidJsonData156>();

		// Token: 0x0400412C RID: 16684
		public static List<BuffSeidJsonData156> DataList = new List<BuffSeidJsonData156>();

		// Token: 0x0400412D RID: 16685
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData156.OnInitFinish);

		// Token: 0x0400412E RID: 16686
		public int id;

		// Token: 0x0400412F RID: 16687
		public int target;

		// Token: 0x04004130 RID: 16688
		public int value1;
	}
}
