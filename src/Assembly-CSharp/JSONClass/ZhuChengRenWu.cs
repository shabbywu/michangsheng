using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000994 RID: 2452
	public class ZhuChengRenWu : IJSONClass
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x001D1DD0 File Offset: 0x001CFFD0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ZhuChengRenWu.list)
			{
				try
				{
					ZhuChengRenWu zhuChengRenWu = new ZhuChengRenWu();
					zhuChengRenWu.Id = jsonobject["Id"].I;
					if (ZhuChengRenWu.DataDict.ContainsKey(zhuChengRenWu.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ZhuChengRenWu.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", zhuChengRenWu.Id));
					}
					else
					{
						ZhuChengRenWu.DataDict.Add(zhuChengRenWu.Id, zhuChengRenWu);
						ZhuChengRenWu.DataList.Add(zhuChengRenWu);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ZhuChengRenWu.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ZhuChengRenWu.OnInitFinishAction != null)
			{
				ZhuChengRenWu.OnInitFinishAction();
			}
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004619 RID: 17945
		public static Dictionary<int, ZhuChengRenWu> DataDict = new Dictionary<int, ZhuChengRenWu>();

		// Token: 0x0400461A RID: 17946
		public static List<ZhuChengRenWu> DataList = new List<ZhuChengRenWu>();

		// Token: 0x0400461B RID: 17947
		public static Action OnInitFinishAction = new Action(ZhuChengRenWu.OnInitFinish);

		// Token: 0x0400461C RID: 17948
		public int Id;
	}
}
