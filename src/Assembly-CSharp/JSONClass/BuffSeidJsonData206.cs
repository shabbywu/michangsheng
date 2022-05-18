using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B43 RID: 2883
	public class BuffSeidJsonData206 : IJSONClass
	{
		// Token: 0x06004874 RID: 18548 RVA: 0x001EDAF4 File Offset: 0x001EBCF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[206].list)
			{
				try
				{
					BuffSeidJsonData206 buffSeidJsonData = new BuffSeidJsonData206();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData206.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData206.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData206.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData206.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData206.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData206.OnInitFinishAction != null)
			{
				BuffSeidJsonData206.OnInitFinishAction();
			}
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004246 RID: 16966
		public static int SEIDID = 206;

		// Token: 0x04004247 RID: 16967
		public static Dictionary<int, BuffSeidJsonData206> DataDict = new Dictionary<int, BuffSeidJsonData206>();

		// Token: 0x04004248 RID: 16968
		public static List<BuffSeidJsonData206> DataList = new List<BuffSeidJsonData206>();

		// Token: 0x04004249 RID: 16969
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData206.OnInitFinish);

		// Token: 0x0400424A RID: 16970
		public int id;

		// Token: 0x0400424B RID: 16971
		public int value1;

		// Token: 0x0400424C RID: 16972
		public int value2;

		// Token: 0x0400424D RID: 16973
		public string panduan;
	}
}
