using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B38 RID: 2872
	public class BuffSeidJsonData196 : IJSONClass
	{
		// Token: 0x06004848 RID: 18504 RVA: 0x001ECDC8 File Offset: 0x001EAFC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[196].list)
			{
				try
				{
					BuffSeidJsonData196 buffSeidJsonData = new BuffSeidJsonData196();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData196.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData196.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData196.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData196.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData196.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData196.OnInitFinishAction != null)
			{
				BuffSeidJsonData196.OnInitFinishAction();
			}
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004200 RID: 16896
		public static int SEIDID = 196;

		// Token: 0x04004201 RID: 16897
		public static Dictionary<int, BuffSeidJsonData196> DataDict = new Dictionary<int, BuffSeidJsonData196>();

		// Token: 0x04004202 RID: 16898
		public static List<BuffSeidJsonData196> DataList = new List<BuffSeidJsonData196>();

		// Token: 0x04004203 RID: 16899
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData196.OnInitFinish);

		// Token: 0x04004204 RID: 16900
		public int id;

		// Token: 0x04004205 RID: 16901
		public int value1;

		// Token: 0x04004206 RID: 16902
		public string panduan;
	}
}
