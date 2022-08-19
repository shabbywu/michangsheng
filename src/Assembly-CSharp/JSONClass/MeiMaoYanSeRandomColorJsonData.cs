using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088C RID: 2188
	public class MeiMaoYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004043 RID: 16451 RVA: 0x001B6B38 File Offset: 0x001B4D38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MeiMaoYanSeRandomColorJsonData.list)
			{
				try
				{
					MeiMaoYanSeRandomColorJsonData meiMaoYanSeRandomColorJsonData = new MeiMaoYanSeRandomColorJsonData();
					meiMaoYanSeRandomColorJsonData.id = jsonobject["id"].I;
					meiMaoYanSeRandomColorJsonData.R = jsonobject["R"].I;
					meiMaoYanSeRandomColorJsonData.G = jsonobject["G"].I;
					meiMaoYanSeRandomColorJsonData.B = jsonobject["B"].I;
					meiMaoYanSeRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (MeiMaoYanSeRandomColorJsonData.DataDict.ContainsKey(meiMaoYanSeRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MeiMaoYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", meiMaoYanSeRandomColorJsonData.id));
					}
					else
					{
						MeiMaoYanSeRandomColorJsonData.DataDict.Add(meiMaoYanSeRandomColorJsonData.id, meiMaoYanSeRandomColorJsonData);
						MeiMaoYanSeRandomColorJsonData.DataList.Add(meiMaoYanSeRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MeiMaoYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MeiMaoYanSeRandomColorJsonData.OnInitFinishAction != null)
			{
				MeiMaoYanSeRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D77 RID: 15735
		public static Dictionary<int, MeiMaoYanSeRandomColorJsonData> DataDict = new Dictionary<int, MeiMaoYanSeRandomColorJsonData>();

		// Token: 0x04003D78 RID: 15736
		public static List<MeiMaoYanSeRandomColorJsonData> DataList = new List<MeiMaoYanSeRandomColorJsonData>();

		// Token: 0x04003D79 RID: 15737
		public static Action OnInitFinishAction = new Action(MeiMaoYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x04003D7A RID: 15738
		public int id;

		// Token: 0x04003D7B RID: 15739
		public int R;

		// Token: 0x04003D7C RID: 15740
		public int G;

		// Token: 0x04003D7D RID: 15741
		public int B;

		// Token: 0x04003D7E RID: 15742
		public string beizhu;
	}
}
