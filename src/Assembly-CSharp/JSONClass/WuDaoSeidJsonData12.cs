using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D05 RID: 3333
	public class WuDaoSeidJsonData12 : IJSONClass
	{
		// Token: 0x06004F7E RID: 20350 RVA: 0x00215870 File Offset: 0x00213A70
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[12].list)
			{
				try
				{
					WuDaoSeidJsonData12 wuDaoSeidJsonData = new WuDaoSeidJsonData12();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData12.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData12.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData12.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData12.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050AD RID: 20653
		public static int SEIDID = 12;

		// Token: 0x040050AE RID: 20654
		public static Dictionary<int, WuDaoSeidJsonData12> DataDict = new Dictionary<int, WuDaoSeidJsonData12>();

		// Token: 0x040050AF RID: 20655
		public static List<WuDaoSeidJsonData12> DataList = new List<WuDaoSeidJsonData12>();

		// Token: 0x040050B0 RID: 20656
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData12.OnInitFinish);

		// Token: 0x040050B1 RID: 20657
		public int skillid;

		// Token: 0x040050B2 RID: 20658
		public int value1;
	}
}
