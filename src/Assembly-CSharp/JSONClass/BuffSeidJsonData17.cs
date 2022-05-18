using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B25 RID: 2853
	public class BuffSeidJsonData17 : IJSONClass
	{
		// Token: 0x060047FC RID: 18428 RVA: 0x001EB668 File Offset: 0x001E9868
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[17].list)
			{
				try
				{
					BuffSeidJsonData17 buffSeidJsonData = new BuffSeidJsonData17();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData17.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData17.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData17.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData17.OnInitFinishAction != null)
			{
				BuffSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004181 RID: 16769
		public static int SEIDID = 17;

		// Token: 0x04004182 RID: 16770
		public static Dictionary<int, BuffSeidJsonData17> DataDict = new Dictionary<int, BuffSeidJsonData17>();

		// Token: 0x04004183 RID: 16771
		public static List<BuffSeidJsonData17> DataList = new List<BuffSeidJsonData17>();

		// Token: 0x04004184 RID: 16772
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData17.OnInitFinish);

		// Token: 0x04004185 RID: 16773
		public int id;

		// Token: 0x04004186 RID: 16774
		public int value1;

		// Token: 0x04004187 RID: 16775
		public int value2;
	}
}
