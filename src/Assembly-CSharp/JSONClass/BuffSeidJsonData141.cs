using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0F RID: 2831
	public class BuffSeidJsonData141 : IJSONClass
	{
		// Token: 0x060047A6 RID: 18342 RVA: 0x001E9C08 File Offset: 0x001E7E08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[141].list)
			{
				try
				{
					BuffSeidJsonData141 buffSeidJsonData = new BuffSeidJsonData141();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData141.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData141.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData141.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData141.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData141.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData141.OnInitFinishAction != null)
			{
				BuffSeidJsonData141.OnInitFinishAction();
			}
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040EE RID: 16622
		public static int SEIDID = 141;

		// Token: 0x040040EF RID: 16623
		public static Dictionary<int, BuffSeidJsonData141> DataDict = new Dictionary<int, BuffSeidJsonData141>();

		// Token: 0x040040F0 RID: 16624
		public static List<BuffSeidJsonData141> DataList = new List<BuffSeidJsonData141>();

		// Token: 0x040040F1 RID: 16625
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData141.OnInitFinish);

		// Token: 0x040040F2 RID: 16626
		public int id;

		// Token: 0x040040F3 RID: 16627
		public int value1;
	}
}
