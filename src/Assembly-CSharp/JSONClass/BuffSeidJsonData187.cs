using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B32 RID: 2866
	public class BuffSeidJsonData187 : IJSONClass
	{
		// Token: 0x06004830 RID: 18480 RVA: 0x001EC640 File Offset: 0x001EA840
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[187].list)
			{
				try
				{
					BuffSeidJsonData187 buffSeidJsonData = new BuffSeidJsonData187();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData187.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData187.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData187.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData187.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData187.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData187.OnInitFinishAction != null)
			{
				BuffSeidJsonData187.OnInitFinishAction();
			}
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041D7 RID: 16855
		public static int SEIDID = 187;

		// Token: 0x040041D8 RID: 16856
		public static Dictionary<int, BuffSeidJsonData187> DataDict = new Dictionary<int, BuffSeidJsonData187>();

		// Token: 0x040041D9 RID: 16857
		public static List<BuffSeidJsonData187> DataList = new List<BuffSeidJsonData187>();

		// Token: 0x040041DA RID: 16858
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData187.OnInitFinish);

		// Token: 0x040041DB RID: 16859
		public int id;

		// Token: 0x040041DC RID: 16860
		public int value1;
	}
}
