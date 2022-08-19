using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200078B RID: 1931
	public class BuffSeidJsonData168 : IJSONClass
	{
		// Token: 0x06003C3E RID: 15422 RVA: 0x0019DF00 File Offset: 0x0019C100
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[168].list)
			{
				try
				{
					BuffSeidJsonData168 buffSeidJsonData = new BuffSeidJsonData168();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData168.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData168.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData168.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData168.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData168.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData168.OnInitFinishAction != null)
			{
				BuffSeidJsonData168.OnInitFinishAction();
			}
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035D9 RID: 13785
		public static int SEIDID = 168;

		// Token: 0x040035DA RID: 13786
		public static Dictionary<int, BuffSeidJsonData168> DataDict = new Dictionary<int, BuffSeidJsonData168>();

		// Token: 0x040035DB RID: 13787
		public static List<BuffSeidJsonData168> DataList = new List<BuffSeidJsonData168>();

		// Token: 0x040035DC RID: 13788
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData168.OnInitFinish);

		// Token: 0x040035DD RID: 13789
		public int id;

		// Token: 0x040035DE RID: 13790
		public int target;

		// Token: 0x040035DF RID: 13791
		public int value1;

		// Token: 0x040035E0 RID: 13792
		public int value2;

		// Token: 0x040035E1 RID: 13793
		public string panduan;
	}
}
