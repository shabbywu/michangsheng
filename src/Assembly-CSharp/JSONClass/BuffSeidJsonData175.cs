using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2A RID: 2858
	public class BuffSeidJsonData175 : IJSONClass
	{
		// Token: 0x06004810 RID: 18448 RVA: 0x001EBC80 File Offset: 0x001E9E80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[175].list)
			{
				try
				{
					BuffSeidJsonData175 buffSeidJsonData = new BuffSeidJsonData175();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData175.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData175.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData175.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData175.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData175.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData175.OnInitFinishAction != null)
			{
				BuffSeidJsonData175.OnInitFinishAction();
			}
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041A2 RID: 16802
		public static int SEIDID = 175;

		// Token: 0x040041A3 RID: 16803
		public static Dictionary<int, BuffSeidJsonData175> DataDict = new Dictionary<int, BuffSeidJsonData175>();

		// Token: 0x040041A4 RID: 16804
		public static List<BuffSeidJsonData175> DataList = new List<BuffSeidJsonData175>();

		// Token: 0x040041A5 RID: 16805
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData175.OnInitFinish);

		// Token: 0x040041A6 RID: 16806
		public int id;

		// Token: 0x040041A7 RID: 16807
		public int value1;
	}
}
