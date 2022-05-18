using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B01 RID: 2817
	public class BuffSeidJsonData123 : IJSONClass
	{
		// Token: 0x0600476E RID: 18286 RVA: 0x001E8A40 File Offset: 0x001E6C40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[123].list)
			{
				try
				{
					BuffSeidJsonData123 buffSeidJsonData = new BuffSeidJsonData123();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData123.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData123.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData123.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData123.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData123.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData123.OnInitFinishAction != null)
			{
				BuffSeidJsonData123.OnInitFinishAction();
			}
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004089 RID: 16521
		public static int SEIDID = 123;

		// Token: 0x0400408A RID: 16522
		public static Dictionary<int, BuffSeidJsonData123> DataDict = new Dictionary<int, BuffSeidJsonData123>();

		// Token: 0x0400408B RID: 16523
		public static List<BuffSeidJsonData123> DataList = new List<BuffSeidJsonData123>();

		// Token: 0x0400408C RID: 16524
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData123.OnInitFinish);

		// Token: 0x0400408D RID: 16525
		public int id;

		// Token: 0x0400408E RID: 16526
		public int value1;
	}
}
