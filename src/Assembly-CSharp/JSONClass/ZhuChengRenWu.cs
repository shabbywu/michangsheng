using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D14 RID: 3348
	public class ZhuChengRenWu : IJSONClass
	{
		// Token: 0x06004FBA RID: 20410 RVA: 0x00216B50 File Offset: 0x00214D50
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

		// Token: 0x06004FBB RID: 20411 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400510D RID: 20749
		public static Dictionary<int, ZhuChengRenWu> DataDict = new Dictionary<int, ZhuChengRenWu>();

		// Token: 0x0400510E RID: 20750
		public static List<ZhuChengRenWu> DataList = new List<ZhuChengRenWu>();

		// Token: 0x0400510F RID: 20751
		public static Action OnInitFinishAction = new Action(ZhuChengRenWu.OnInitFinish);

		// Token: 0x04005110 RID: 20752
		public int Id;
	}
}
