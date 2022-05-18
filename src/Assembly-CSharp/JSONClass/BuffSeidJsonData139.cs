using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0D RID: 2829
	public class BuffSeidJsonData139 : IJSONClass
	{
		// Token: 0x0600479E RID: 18334 RVA: 0x001E9984 File Offset: 0x001E7B84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[139].list)
			{
				try
				{
					BuffSeidJsonData139 buffSeidJsonData = new BuffSeidJsonData139();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData139.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData139.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData139.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData139.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData139.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData139.OnInitFinishAction != null)
			{
				BuffSeidJsonData139.OnInitFinishAction();
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040E0 RID: 16608
		public static int SEIDID = 139;

		// Token: 0x040040E1 RID: 16609
		public static Dictionary<int, BuffSeidJsonData139> DataDict = new Dictionary<int, BuffSeidJsonData139>();

		// Token: 0x040040E2 RID: 16610
		public static List<BuffSeidJsonData139> DataList = new List<BuffSeidJsonData139>();

		// Token: 0x040040E3 RID: 16611
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData139.OnInitFinish);

		// Token: 0x040040E4 RID: 16612
		public int id;

		// Token: 0x040040E5 RID: 16613
		public int value2;
	}
}
