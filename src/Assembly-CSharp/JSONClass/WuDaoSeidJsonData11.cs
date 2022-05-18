using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D04 RID: 3332
	public class WuDaoSeidJsonData11 : IJSONClass
	{
		// Token: 0x06004F7A RID: 20346 RVA: 0x00215748 File Offset: 0x00213948
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[11].list)
			{
				try
				{
					WuDaoSeidJsonData11 wuDaoSeidJsonData = new WuDaoSeidJsonData11();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData11.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData11.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData11.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData11.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050A7 RID: 20647
		public static int SEIDID = 11;

		// Token: 0x040050A8 RID: 20648
		public static Dictionary<int, WuDaoSeidJsonData11> DataDict = new Dictionary<int, WuDaoSeidJsonData11>();

		// Token: 0x040050A9 RID: 20649
		public static List<WuDaoSeidJsonData11> DataList = new List<WuDaoSeidJsonData11>();

		// Token: 0x040050AA RID: 20650
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData11.OnInitFinish);

		// Token: 0x040050AB RID: 20651
		public int skillid;

		// Token: 0x040050AC RID: 20652
		public int value1;
	}
}
