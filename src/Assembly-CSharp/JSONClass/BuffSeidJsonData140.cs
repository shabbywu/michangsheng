using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0E RID: 2830
	public class BuffSeidJsonData140 : IJSONClass
	{
		// Token: 0x060047A2 RID: 18338 RVA: 0x001E9AB0 File Offset: 0x001E7CB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[140].list)
			{
				try
				{
					BuffSeidJsonData140 buffSeidJsonData = new BuffSeidJsonData140();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData140.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData140.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData140.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData140.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData140.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData140.OnInitFinishAction != null)
			{
				BuffSeidJsonData140.OnInitFinishAction();
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040E6 RID: 16614
		public static int SEIDID = 140;

		// Token: 0x040040E7 RID: 16615
		public static Dictionary<int, BuffSeidJsonData140> DataDict = new Dictionary<int, BuffSeidJsonData140>();

		// Token: 0x040040E8 RID: 16616
		public static List<BuffSeidJsonData140> DataList = new List<BuffSeidJsonData140>();

		// Token: 0x040040E9 RID: 16617
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData140.OnInitFinish);

		// Token: 0x040040EA RID: 16618
		public int id;

		// Token: 0x040040EB RID: 16619
		public int value1;

		// Token: 0x040040EC RID: 16620
		public int value2;

		// Token: 0x040040ED RID: 16621
		public int value3;
	}
}
