using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFE RID: 2814
	public class BuffSeidJsonData118 : IJSONClass
	{
		// Token: 0x06004762 RID: 18274 RVA: 0x001E869C File Offset: 0x001E689C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[118].list)
			{
				try
				{
					BuffSeidJsonData118 buffSeidJsonData = new BuffSeidJsonData118();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData118.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData118.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData118.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData118.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData118.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData118.OnInitFinishAction != null)
			{
				BuffSeidJsonData118.OnInitFinishAction();
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004075 RID: 16501
		public static int SEIDID = 118;

		// Token: 0x04004076 RID: 16502
		public static Dictionary<int, BuffSeidJsonData118> DataDict = new Dictionary<int, BuffSeidJsonData118>();

		// Token: 0x04004077 RID: 16503
		public static List<BuffSeidJsonData118> DataList = new List<BuffSeidJsonData118>();

		// Token: 0x04004078 RID: 16504
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData118.OnInitFinish);

		// Token: 0x04004079 RID: 16505
		public int id;

		// Token: 0x0400407A RID: 16506
		public int value1;
	}
}
