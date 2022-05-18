using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D06 RID: 3334
	public class WuDaoSeidJsonData14 : IJSONClass
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x00215998 File Offset: 0x00213B98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[14].list)
			{
				try
				{
					WuDaoSeidJsonData14 wuDaoSeidJsonData = new WuDaoSeidJsonData14();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData14.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData14.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData14.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData14.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050B3 RID: 20659
		public static int SEIDID = 14;

		// Token: 0x040050B4 RID: 20660
		public static Dictionary<int, WuDaoSeidJsonData14> DataDict = new Dictionary<int, WuDaoSeidJsonData14>();

		// Token: 0x040050B5 RID: 20661
		public static List<WuDaoSeidJsonData14> DataList = new List<WuDaoSeidJsonData14>();

		// Token: 0x040050B6 RID: 20662
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData14.OnInitFinish);

		// Token: 0x040050B7 RID: 20663
		public int skillid;

		// Token: 0x040050B8 RID: 20664
		public int value1;
	}
}
