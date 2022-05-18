using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2B RID: 2859
	public class BuffSeidJsonData176 : IJSONClass
	{
		// Token: 0x06004814 RID: 18452 RVA: 0x001EBDAC File Offset: 0x001E9FAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[176].list)
			{
				try
				{
					BuffSeidJsonData176 buffSeidJsonData = new BuffSeidJsonData176();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData176.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData176.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData176.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData176.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData176.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData176.OnInitFinishAction != null)
			{
				BuffSeidJsonData176.OnInitFinishAction();
			}
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041A8 RID: 16808
		public static int SEIDID = 176;

		// Token: 0x040041A9 RID: 16809
		public static Dictionary<int, BuffSeidJsonData176> DataDict = new Dictionary<int, BuffSeidJsonData176>();

		// Token: 0x040041AA RID: 16810
		public static List<BuffSeidJsonData176> DataList = new List<BuffSeidJsonData176>();

		// Token: 0x040041AB RID: 16811
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData176.OnInitFinish);

		// Token: 0x040041AC RID: 16812
		public int id;

		// Token: 0x040041AD RID: 16813
		public int target;

		// Token: 0x040041AE RID: 16814
		public int value1;
	}
}
