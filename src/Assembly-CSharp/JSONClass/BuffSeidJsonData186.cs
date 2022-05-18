using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B31 RID: 2865
	public class BuffSeidJsonData186 : IJSONClass
	{
		// Token: 0x0600482C RID: 18476 RVA: 0x001EC500 File Offset: 0x001EA700
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[186].list)
			{
				try
				{
					BuffSeidJsonData186 buffSeidJsonData = new BuffSeidJsonData186();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData186.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData186.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData186.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData186.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData186.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData186.OnInitFinishAction != null)
			{
				BuffSeidJsonData186.OnInitFinishAction();
			}
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041D0 RID: 16848
		public static int SEIDID = 186;

		// Token: 0x040041D1 RID: 16849
		public static Dictionary<int, BuffSeidJsonData186> DataDict = new Dictionary<int, BuffSeidJsonData186>();

		// Token: 0x040041D2 RID: 16850
		public static List<BuffSeidJsonData186> DataList = new List<BuffSeidJsonData186>();

		// Token: 0x040041D3 RID: 16851
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData186.OnInitFinish);

		// Token: 0x040041D4 RID: 16852
		public int id;

		// Token: 0x040041D5 RID: 16853
		public int value1;

		// Token: 0x040041D6 RID: 16854
		public int value2;
	}
}
