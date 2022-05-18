using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B08 RID: 2824
	public class BuffSeidJsonData134 : IJSONClass
	{
		// Token: 0x0600478A RID: 18314 RVA: 0x001E9328 File Offset: 0x001E7528
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[134].list)
			{
				try
				{
					BuffSeidJsonData134 buffSeidJsonData = new BuffSeidJsonData134();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData134.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData134.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData134.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData134.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData134.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData134.OnInitFinishAction != null)
			{
				BuffSeidJsonData134.OnInitFinishAction();
			}
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040BC RID: 16572
		public static int SEIDID = 134;

		// Token: 0x040040BD RID: 16573
		public static Dictionary<int, BuffSeidJsonData134> DataDict = new Dictionary<int, BuffSeidJsonData134>();

		// Token: 0x040040BE RID: 16574
		public static List<BuffSeidJsonData134> DataList = new List<BuffSeidJsonData134>();

		// Token: 0x040040BF RID: 16575
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData134.OnInitFinish);

		// Token: 0x040040C0 RID: 16576
		public int id;

		// Token: 0x040040C1 RID: 16577
		public int target;

		// Token: 0x040040C2 RID: 16578
		public int value1;

		// Token: 0x040040C3 RID: 16579
		public int value2;
	}
}
