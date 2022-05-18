using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1E RID: 2846
	public class BuffSeidJsonData162 : IJSONClass
	{
		// Token: 0x060047E0 RID: 18400 RVA: 0x001EADC8 File Offset: 0x001E8FC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[162].list)
			{
				try
				{
					BuffSeidJsonData162 buffSeidJsonData = new BuffSeidJsonData162();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData162.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData162.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData162.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData162.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData162.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData162.OnInitFinishAction != null)
			{
				BuffSeidJsonData162.OnInitFinishAction();
			}
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004153 RID: 16723
		public static int SEIDID = 162;

		// Token: 0x04004154 RID: 16724
		public static Dictionary<int, BuffSeidJsonData162> DataDict = new Dictionary<int, BuffSeidJsonData162>();

		// Token: 0x04004155 RID: 16725
		public static List<BuffSeidJsonData162> DataList = new List<BuffSeidJsonData162>();

		// Token: 0x04004156 RID: 16726
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData162.OnInitFinish);

		// Token: 0x04004157 RID: 16727
		public int id;

		// Token: 0x04004158 RID: 16728
		public int value1;
	}
}
