using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3A RID: 2874
	public class BuffSeidJsonData199 : IJSONClass
	{
		// Token: 0x06004850 RID: 18512 RVA: 0x001ED048 File Offset: 0x001EB248
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[199].list)
			{
				try
				{
					BuffSeidJsonData199 buffSeidJsonData = new BuffSeidJsonData199();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData199.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData199.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData199.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData199.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData199.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData199.OnInitFinishAction != null)
			{
				BuffSeidJsonData199.OnInitFinishAction();
			}
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400420E RID: 16910
		public static int SEIDID = 199;

		// Token: 0x0400420F RID: 16911
		public static Dictionary<int, BuffSeidJsonData199> DataDict = new Dictionary<int, BuffSeidJsonData199>();

		// Token: 0x04004210 RID: 16912
		public static List<BuffSeidJsonData199> DataList = new List<BuffSeidJsonData199>();

		// Token: 0x04004211 RID: 16913
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData199.OnInitFinish);

		// Token: 0x04004212 RID: 16914
		public int id;

		// Token: 0x04004213 RID: 16915
		public int value1;

		// Token: 0x04004214 RID: 16916
		public int value2;
	}
}
