using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3D RID: 2877
	public class BuffSeidJsonData200 : IJSONClass
	{
		// Token: 0x0600485C RID: 18524 RVA: 0x001ED3D8 File Offset: 0x001EB5D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[200].list)
			{
				try
				{
					BuffSeidJsonData200 buffSeidJsonData = new BuffSeidJsonData200();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData200.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData200.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData200.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData200.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData200.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData200.OnInitFinishAction != null)
			{
				BuffSeidJsonData200.OnInitFinishAction();
			}
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004221 RID: 16929
		public static int SEIDID = 200;

		// Token: 0x04004222 RID: 16930
		public static Dictionary<int, BuffSeidJsonData200> DataDict = new Dictionary<int, BuffSeidJsonData200>();

		// Token: 0x04004223 RID: 16931
		public static List<BuffSeidJsonData200> DataList = new List<BuffSeidJsonData200>();

		// Token: 0x04004224 RID: 16932
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData200.OnInitFinish);

		// Token: 0x04004225 RID: 16933
		public int id;

		// Token: 0x04004226 RID: 16934
		public int value1;
	}
}
