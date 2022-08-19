using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000783 RID: 1923
	public class BuffSeidJsonData159 : IJSONClass
	{
		// Token: 0x06003C1F RID: 15391 RVA: 0x0019D404 File Offset: 0x0019B604
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[159].list)
			{
				try
				{
					BuffSeidJsonData159 buffSeidJsonData = new BuffSeidJsonData159();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData159.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData159.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData159.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData159.OnInitFinishAction != null)
			{
				BuffSeidJsonData159.OnInitFinishAction();
			}
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035A5 RID: 13733
		public static int SEIDID = 159;

		// Token: 0x040035A6 RID: 13734
		public static Dictionary<int, BuffSeidJsonData159> DataDict = new Dictionary<int, BuffSeidJsonData159>();

		// Token: 0x040035A7 RID: 13735
		public static List<BuffSeidJsonData159> DataList = new List<BuffSeidJsonData159>();

		// Token: 0x040035A8 RID: 13736
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData159.OnInitFinish);

		// Token: 0x040035A9 RID: 13737
		public int id;

		// Token: 0x040035AA RID: 13738
		public int target;

		// Token: 0x040035AB RID: 13739
		public int value1;

		// Token: 0x040035AC RID: 13740
		public int value2;

		// Token: 0x040035AD RID: 13741
		public string panduan;
	}
}
