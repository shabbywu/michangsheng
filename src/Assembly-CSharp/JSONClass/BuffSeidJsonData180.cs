using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B2F RID: 2863
	public class BuffSeidJsonData180 : IJSONClass
	{
		// Token: 0x06004824 RID: 18468 RVA: 0x001EC280 File Offset: 0x001EA480
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[180].list)
			{
				try
				{
					BuffSeidJsonData180 buffSeidJsonData = new BuffSeidJsonData180();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData180.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData180.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData180.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData180.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData180.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData180.OnInitFinishAction != null)
			{
				BuffSeidJsonData180.OnInitFinishAction();
			}
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041C2 RID: 16834
		public static int SEIDID = 180;

		// Token: 0x040041C3 RID: 16835
		public static Dictionary<int, BuffSeidJsonData180> DataDict = new Dictionary<int, BuffSeidJsonData180>();

		// Token: 0x040041C4 RID: 16836
		public static List<BuffSeidJsonData180> DataList = new List<BuffSeidJsonData180>();

		// Token: 0x040041C5 RID: 16837
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData180.OnInitFinish);

		// Token: 0x040041C6 RID: 16838
		public int id;

		// Token: 0x040041C7 RID: 16839
		public int value1;

		// Token: 0x040041C8 RID: 16840
		public string panduan;
	}
}
