using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3B RID: 2875
	public class BuffSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004854 RID: 18516 RVA: 0x001ED188 File Offset: 0x001EB388
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[2].list)
			{
				try
				{
					BuffSeidJsonData2 buffSeidJsonData = new BuffSeidJsonData2();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData2.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData2.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData2.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData2.OnInitFinishAction != null)
			{
				BuffSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004215 RID: 16917
		public static int SEIDID = 2;

		// Token: 0x04004216 RID: 16918
		public static Dictionary<int, BuffSeidJsonData2> DataDict = new Dictionary<int, BuffSeidJsonData2>();

		// Token: 0x04004217 RID: 16919
		public static List<BuffSeidJsonData2> DataList = new List<BuffSeidJsonData2>();

		// Token: 0x04004218 RID: 16920
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData2.OnInitFinish);

		// Token: 0x04004219 RID: 16921
		public int id;

		// Token: 0x0400421A RID: 16922
		public int value1;
	}
}
