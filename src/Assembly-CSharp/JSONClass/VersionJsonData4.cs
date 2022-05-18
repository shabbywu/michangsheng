using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFC RID: 3324
	public class VersionJsonData4 : IJSONClass
	{
		// Token: 0x06004F5A RID: 20314 RVA: 0x00214B34 File Offset: 0x00212D34
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

		// Token: 0x06004F5B RID: 20315 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400505F RID: 20575
		public static int SEIDID = 4;

		// Token: 0x04005060 RID: 20576
		public static Dictionary<int, VersionJsonData4> DataDict = new Dictionary<int, VersionJsonData4>();

		// Token: 0x04005061 RID: 20577
		public static List<VersionJsonData4> DataList = new List<VersionJsonData4>();

		// Token: 0x04005062 RID: 20578
		public static Action OnInitFinishAction = new Action(VersionJsonData4.OnInitFinish);

		// Token: 0x04005063 RID: 20579
		public int id;

		// Token: 0x04005064 RID: 20580
		public int XueLiang;

		// Token: 0x04005065 RID: 20581
		public int ShenShi;

		// Token: 0x04005066 RID: 20582
		public int DunSu;
	}
}
