using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1B RID: 2843
	public class BuffSeidJsonData159 : IJSONClass
	{
		// Token: 0x060047D5 RID: 18389 RVA: 0x001EAA58 File Offset: 0x001E8C58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[159].list)
			{
				try
				{
					BuffSeidJsonData159 buffSeidJsonData = new BuffSeidJsonData159();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData159.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData159.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData159.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData159.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData159.OnInitFinishAction != null)
			{
				BuffSeidJsonData159.OnInitFinishAction();
			}
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400413E RID: 16702
		public static int SEIDID = 159;

		// Token: 0x0400413F RID: 16703
		public static Dictionary<int, BuffSeidJsonData159> DataDict = new Dictionary<int, BuffSeidJsonData159>();

		// Token: 0x04004140 RID: 16704
		public static List<BuffSeidJsonData159> DataList = new List<BuffSeidJsonData159>();

		// Token: 0x04004141 RID: 16705
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData159.OnInitFinish);

		// Token: 0x04004142 RID: 16706
		public int id;

		// Token: 0x04004143 RID: 16707
		public int target;

		// Token: 0x04004144 RID: 16708
		public int value1;

		// Token: 0x04004145 RID: 16709
		public int value2;

		// Token: 0x04004146 RID: 16710
		public string panduan;
	}
}
