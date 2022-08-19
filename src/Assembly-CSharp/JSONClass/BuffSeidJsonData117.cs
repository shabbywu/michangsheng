using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000765 RID: 1893
	public class BuffSeidJsonData117 : IJSONClass
	{
		// Token: 0x06003BA8 RID: 15272 RVA: 0x0019A8A4 File Offset: 0x00198AA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[117].list)
			{
				try
				{
					BuffSeidJsonData117 buffSeidJsonData = new BuffSeidJsonData117();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData117.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData117.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData117.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData117.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData117.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData117.OnInitFinishAction != null)
			{
				BuffSeidJsonData117.OnInitFinishAction();
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040034D5 RID: 13525
		public static int SEIDID = 117;

		// Token: 0x040034D6 RID: 13526
		public static Dictionary<int, BuffSeidJsonData117> DataDict = new Dictionary<int, BuffSeidJsonData117>();

		// Token: 0x040034D7 RID: 13527
		public static List<BuffSeidJsonData117> DataList = new List<BuffSeidJsonData117>();

		// Token: 0x040034D8 RID: 13528
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData117.OnInitFinish);

		// Token: 0x040034D9 RID: 13529
		public int id;

		// Token: 0x040034DA RID: 13530
		public int value1;

		// Token: 0x040034DB RID: 13531
		public int value2;
	}
}
