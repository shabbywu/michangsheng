using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000790 RID: 1936
	public class BuffSeidJsonData172 : IJSONClass
	{
		// Token: 0x06003C52 RID: 15442 RVA: 0x0019E670 File Offset: 0x0019C870
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[172].list)
			{
				try
				{
					BuffSeidJsonData172 buffSeidJsonData = new BuffSeidJsonData172();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData172.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData172.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData172.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData172.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData172.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData172.OnInitFinishAction != null)
			{
				BuffSeidJsonData172.OnInitFinishAction();
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035FD RID: 13821
		public static int SEIDID = 172;

		// Token: 0x040035FE RID: 13822
		public static Dictionary<int, BuffSeidJsonData172> DataDict = new Dictionary<int, BuffSeidJsonData172>();

		// Token: 0x040035FF RID: 13823
		public static List<BuffSeidJsonData172> DataList = new List<BuffSeidJsonData172>();

		// Token: 0x04003600 RID: 13824
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData172.OnInitFinish);

		// Token: 0x04003601 RID: 13825
		public int id;

		// Token: 0x04003602 RID: 13826
		public int value1;
	}
}
