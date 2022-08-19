using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000807 RID: 2055
	public class ChuanWenTypeData : IJSONClass
	{
		// Token: 0x06003E2E RID: 15918 RVA: 0x001A93CC File Offset: 0x001A75CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ChuanWenTypeData.list)
			{
				try
				{
					ChuanWenTypeData chuanWenTypeData = new ChuanWenTypeData();
					chuanWenTypeData.id = jsonobject["id"].I;
					chuanWenTypeData.ChuanWenType = jsonobject["ChuanWenType"].I;
					if (ChuanWenTypeData.DataDict.ContainsKey(chuanWenTypeData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ChuanWenTypeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", chuanWenTypeData.id));
					}
					else
					{
						ChuanWenTypeData.DataDict.Add(chuanWenTypeData.id, chuanWenTypeData);
						ChuanWenTypeData.DataList.Add(chuanWenTypeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ChuanWenTypeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ChuanWenTypeData.OnInitFinishAction != null)
			{
				ChuanWenTypeData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400393A RID: 14650
		public static Dictionary<int, ChuanWenTypeData> DataDict = new Dictionary<int, ChuanWenTypeData>();

		// Token: 0x0400393B RID: 14651
		public static List<ChuanWenTypeData> DataList = new List<ChuanWenTypeData>();

		// Token: 0x0400393C RID: 14652
		public static Action OnInitFinishAction = new Action(ChuanWenTypeData.OnInitFinish);

		// Token: 0x0400393D RID: 14653
		public int id;

		// Token: 0x0400393E RID: 14654
		public int ChuanWenType;
	}
}
