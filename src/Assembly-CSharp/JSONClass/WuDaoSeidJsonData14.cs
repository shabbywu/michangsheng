using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000986 RID: 2438
	public class WuDaoSeidJsonData14 : IJSONClass
	{
		// Token: 0x0600442C RID: 17452 RVA: 0x001D0980 File Offset: 0x001CEB80
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

		// Token: 0x0600442D RID: 17453 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045BF RID: 17855
		public static int SEIDID = 14;

		// Token: 0x040045C0 RID: 17856
		public static Dictionary<int, WuDaoSeidJsonData14> DataDict = new Dictionary<int, WuDaoSeidJsonData14>();

		// Token: 0x040045C1 RID: 17857
		public static List<WuDaoSeidJsonData14> DataList = new List<WuDaoSeidJsonData14>();

		// Token: 0x040045C2 RID: 17858
		public static Action OnInitFinishAction = new Action(WuDaoSeidJsonData14.OnInitFinish);

		// Token: 0x040045C3 RID: 17859
		public int skillid;

		// Token: 0x040045C4 RID: 17860
		public int value1;
	}
}
