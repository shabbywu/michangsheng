using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B22 RID: 2850
	public class BuffSeidJsonData167 : IJSONClass
	{
		// Token: 0x060047F0 RID: 18416 RVA: 0x001EB278 File Offset: 0x001E9478
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[167].list)
			{
				try
				{
					BuffSeidJsonData167 buffSeidJsonData = new BuffSeidJsonData167();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData167.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData167.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData167.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData167.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData167.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData167.OnInitFinishAction != null)
			{
				BuffSeidJsonData167.OnInitFinishAction();
			}
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400416B RID: 16747
		public static int SEIDID = 167;

		// Token: 0x0400416C RID: 16748
		public static Dictionary<int, BuffSeidJsonData167> DataDict = new Dictionary<int, BuffSeidJsonData167>();

		// Token: 0x0400416D RID: 16749
		public static List<BuffSeidJsonData167> DataList = new List<BuffSeidJsonData167>();

		// Token: 0x0400416E RID: 16750
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData167.OnInitFinish);

		// Token: 0x0400416F RID: 16751
		public int id;

		// Token: 0x04004170 RID: 16752
		public int target;

		// Token: 0x04004171 RID: 16753
		public int value1;
	}
}
