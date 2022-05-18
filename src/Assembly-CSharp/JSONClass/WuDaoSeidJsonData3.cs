using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D08 RID: 3336
	public class WuDaoSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004F8A RID: 20362 RVA: 0x00215BE8 File Offset: 0x00213DE8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoSeidJsonData[3].list)
			{
				try
				{
					WuDaoSeidJsonData3 wuDaoSeidJsonData = new WuDaoSeidJsonData3();
					wuDaoSeidJsonData.skillid = jsonobject["skillid"].I;
					wuDaoSeidJsonData.value1 = jsonobject["value1"].I;
					if (WuDaoSeidJsonData3.DataDict.ContainsKey(wuDaoSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoSeidJsonData.skillid));
					}
					else
					{
						WuDaoSeidJsonData3.DataDict.Add(wuDaoSeidJsonData.skillid, wuDaoSeidJsonData);
						WuDaoSeidJsonData3.DataList.Add(wuDaoSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoSeidJsonData3.OnInitFinishAction != null)
			{
				WuDaoSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050BF RID: 20671
		public static int SEIDID = 3;

		// Token: 0x040050C0 RID: 20672
		public static Dictionary<int, WuDaoSeidJsonData3> DataDict = new Dictionary<int, WuDaoSeidJsonData3>();

		// Token: 0x040050C1 RID: 20673
		public static List<WuDaoSeidJsonData3> DataList = new List<WuDaoSeidJsonData3>();

		// Token: 0x040050C2 RID: 20674
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData3.OnInitFinish);

		// Token: 0x040050C3 RID: 20675
		public int skillid;

		// Token: 0x040050C4 RID: 20676
		public int value1;
	}
}
