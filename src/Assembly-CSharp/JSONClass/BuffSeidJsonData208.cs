using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AC RID: 1964
	public class BuffSeidJsonData208 : IJSONClass
	{
		// Token: 0x06003CC2 RID: 15554 RVA: 0x001A0E84 File Offset: 0x0019F084
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[208].list)
			{
				try
				{
					BuffSeidJsonData208 buffSeidJsonData = new BuffSeidJsonData208();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData208.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData208.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData208.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData208.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData208.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData208.OnInitFinishAction != null)
			{
				BuffSeidJsonData208.OnInitFinishAction();
			}
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036B5 RID: 14005
		public static int SEIDID = 208;

		// Token: 0x040036B6 RID: 14006
		public static Dictionary<int, BuffSeidJsonData208> DataDict = new Dictionary<int, BuffSeidJsonData208>();

		// Token: 0x040036B7 RID: 14007
		public static List<BuffSeidJsonData208> DataList = new List<BuffSeidJsonData208>();

		// Token: 0x040036B8 RID: 14008
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData208.OnInitFinish);

		// Token: 0x040036B9 RID: 14009
		public int id;

		// Token: 0x040036BA RID: 14010
		public int value1;
	}
}
