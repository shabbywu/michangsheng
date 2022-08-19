using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097C RID: 2428
	public class VersionJsonData4 : IJSONClass
	{
		// Token: 0x06004404 RID: 17412 RVA: 0x001CF928 File Offset: 0x001CDB28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.VersionJsonData[4].list)
			{
				try
				{
					VersionJsonData4 versionJsonData = new VersionJsonData4();
					versionJsonData.id = jsonobject["id"].I;
					versionJsonData.XueLiang = jsonobject["XueLiang"].I;
					versionJsonData.ShenShi = jsonobject["ShenShi"].I;
					versionJsonData.DunSu = jsonobject["DunSu"].I;
					if (VersionJsonData4.DataDict.ContainsKey(versionJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典VersionJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", versionJsonData.id));
					}
					else
					{
						VersionJsonData4.DataDict.Add(versionJsonData.id, versionJsonData);
						VersionJsonData4.DataList.Add(versionJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典VersionJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (VersionJsonData4.OnInitFinishAction != null)
			{
				VersionJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400456B RID: 17771
		public static int SEIDID = 4;

		// Token: 0x0400456C RID: 17772
		public static Dictionary<int, VersionJsonData4> DataDict = new Dictionary<int, VersionJsonData4>();

		// Token: 0x0400456D RID: 17773
		public static List<VersionJsonData4> DataList = new List<VersionJsonData4>();

		// Token: 0x0400456E RID: 17774
		public static Action OnInitFinishAction = new Action(VersionJsonData4.OnInitFinish);

		// Token: 0x0400456F RID: 17775
		public int id;

		// Token: 0x04004570 RID: 17776
		public int XueLiang;

		// Token: 0x04004571 RID: 17777
		public int ShenShi;

		// Token: 0x04004572 RID: 17778
		public int DunSu;
	}
}
