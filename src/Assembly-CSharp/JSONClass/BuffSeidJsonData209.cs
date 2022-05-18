using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B45 RID: 2885
	public class BuffSeidJsonData209 : IJSONClass
	{
		// Token: 0x0600487C RID: 18556 RVA: 0x001EDD78 File Offset: 0x001EBF78
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[209].list)
			{
				try
				{
					BuffSeidJsonData209 buffSeidJsonData = new BuffSeidJsonData209();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData209.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData209.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData209.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData209.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData209.OnInitFinishAction != null)
			{
				BuffSeidJsonData209.OnInitFinishAction();
			}
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004254 RID: 16980
		public static int SEIDID = 209;

		// Token: 0x04004255 RID: 16981
		public static Dictionary<int, BuffSeidJsonData209> DataDict = new Dictionary<int, BuffSeidJsonData209>();

		// Token: 0x04004256 RID: 16982
		public static List<BuffSeidJsonData209> DataList = new List<BuffSeidJsonData209>();

		// Token: 0x04004257 RID: 16983
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData209.OnInitFinish);

		// Token: 0x04004258 RID: 16984
		public int id;

		// Token: 0x04004259 RID: 16985
		public int value1;
	}
}
