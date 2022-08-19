using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000788 RID: 1928
	public class BuffSeidJsonData165 : IJSONClass
	{
		// Token: 0x06003C32 RID: 15410 RVA: 0x0019DACC File Offset: 0x0019BCCC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[165].list)
			{
				try
				{
					BuffSeidJsonData165 buffSeidJsonData = new BuffSeidJsonData165();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData165.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData165.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData165.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData165.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData165.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData165.OnInitFinishAction != null)
			{
				BuffSeidJsonData165.OnInitFinishAction();
			}
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035C6 RID: 13766
		public static int SEIDID = 165;

		// Token: 0x040035C7 RID: 13767
		public static Dictionary<int, BuffSeidJsonData165> DataDict = new Dictionary<int, BuffSeidJsonData165>();

		// Token: 0x040035C8 RID: 13768
		public static List<BuffSeidJsonData165> DataList = new List<BuffSeidJsonData165>();

		// Token: 0x040035C9 RID: 13769
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData165.OnInitFinish);

		// Token: 0x040035CA RID: 13770
		public int id;

		// Token: 0x040035CB RID: 13771
		public int value1;
	}
}
