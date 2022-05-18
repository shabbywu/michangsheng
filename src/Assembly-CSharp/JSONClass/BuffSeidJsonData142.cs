using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B10 RID: 2832
	public class BuffSeidJsonData142 : IJSONClass
	{
		// Token: 0x060047AA RID: 18346 RVA: 0x001E9D34 File Offset: 0x001E7F34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[142].list)
			{
				try
				{
					BuffSeidJsonData142 buffSeidJsonData = new BuffSeidJsonData142();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData142.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData142.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData142.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData142.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData142.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData142.OnInitFinishAction != null)
			{
				BuffSeidJsonData142.OnInitFinishAction();
			}
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040F4 RID: 16628
		public static int SEIDID = 142;

		// Token: 0x040040F5 RID: 16629
		public static Dictionary<int, BuffSeidJsonData142> DataDict = new Dictionary<int, BuffSeidJsonData142>();

		// Token: 0x040040F6 RID: 16630
		public static List<BuffSeidJsonData142> DataList = new List<BuffSeidJsonData142>();

		// Token: 0x040040F7 RID: 16631
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData142.OnInitFinish);

		// Token: 0x040040F8 RID: 16632
		public int id;

		// Token: 0x040040F9 RID: 16633
		public int target;

		// Token: 0x040040FA RID: 16634
		public int value1;

		// Token: 0x040040FB RID: 16635
		public int value2;
	}
}
