using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B0C RID: 2828
	public class BuffSeidJsonData138 : IJSONClass
	{
		// Token: 0x0600479A RID: 18330 RVA: 0x001E982C File Offset: 0x001E7A2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[138].list)
			{
				try
				{
					BuffSeidJsonData138 buffSeidJsonData = new BuffSeidJsonData138();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData138.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData138.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData138.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData138.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData138.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData138.OnInitFinishAction != null)
			{
				BuffSeidJsonData138.OnInitFinishAction();
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040D8 RID: 16600
		public static int SEIDID = 138;

		// Token: 0x040040D9 RID: 16601
		public static Dictionary<int, BuffSeidJsonData138> DataDict = new Dictionary<int, BuffSeidJsonData138>();

		// Token: 0x040040DA RID: 16602
		public static List<BuffSeidJsonData138> DataList = new List<BuffSeidJsonData138>();

		// Token: 0x040040DB RID: 16603
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData138.OnInitFinish);

		// Token: 0x040040DC RID: 16604
		public int id;

		// Token: 0x040040DD RID: 16605
		public int target;

		// Token: 0x040040DE RID: 16606
		public int value1;

		// Token: 0x040040DF RID: 16607
		public int value2;
	}
}
