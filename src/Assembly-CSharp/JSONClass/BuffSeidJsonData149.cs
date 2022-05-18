using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B15 RID: 2837
	public class BuffSeidJsonData149 : IJSONClass
	{
		// Token: 0x060047BE RID: 18366 RVA: 0x001EA37C File Offset: 0x001E857C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[149].list)
			{
				try
				{
					BuffSeidJsonData149 buffSeidJsonData = new BuffSeidJsonData149();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData149.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData149.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData149.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData149.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData149.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData149.OnInitFinishAction != null)
			{
				BuffSeidJsonData149.OnInitFinishAction();
			}
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004117 RID: 16663
		public static int SEIDID = 149;

		// Token: 0x04004118 RID: 16664
		public static Dictionary<int, BuffSeidJsonData149> DataDict = new Dictionary<int, BuffSeidJsonData149>();

		// Token: 0x04004119 RID: 16665
		public static List<BuffSeidJsonData149> DataList = new List<BuffSeidJsonData149>();

		// Token: 0x0400411A RID: 16666
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData149.OnInitFinish);

		// Token: 0x0400411B RID: 16667
		public int id;

		// Token: 0x0400411C RID: 16668
		public List<int> value1 = new List<int>();
	}
}
