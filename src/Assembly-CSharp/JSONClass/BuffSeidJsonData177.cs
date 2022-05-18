using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2C RID: 2860
	public class BuffSeidJsonData177 : IJSONClass
	{
		// Token: 0x06004818 RID: 18456 RVA: 0x001EBEEC File Offset: 0x001EA0EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[177].list)
			{
				try
				{
					BuffSeidJsonData177 buffSeidJsonData = new BuffSeidJsonData177();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData177.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData177.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData177.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData177.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData177.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData177.OnInitFinishAction != null)
			{
				BuffSeidJsonData177.OnInitFinishAction();
			}
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041AF RID: 16815
		public static int SEIDID = 177;

		// Token: 0x040041B0 RID: 16816
		public static Dictionary<int, BuffSeidJsonData177> DataDict = new Dictionary<int, BuffSeidJsonData177>();

		// Token: 0x040041B1 RID: 16817
		public static List<BuffSeidJsonData177> DataList = new List<BuffSeidJsonData177>();

		// Token: 0x040041B2 RID: 16818
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData177.OnInitFinish);

		// Token: 0x040041B3 RID: 16819
		public int id;

		// Token: 0x040041B4 RID: 16820
		public int value1;

		// Token: 0x040041B5 RID: 16821
		public int value2;
	}
}
