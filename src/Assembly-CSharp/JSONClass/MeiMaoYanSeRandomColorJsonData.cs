using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1A RID: 3098
	public class MeiMaoYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004BD1 RID: 19409 RVA: 0x002000D4 File Offset: 0x001FE2D4
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

		// Token: 0x06004BD2 RID: 19410 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048D0 RID: 18640
		public static Dictionary<int, MeiMaoYanSeRandomColorJsonData> DataDict = new Dictionary<int, MeiMaoYanSeRandomColorJsonData>();

		// Token: 0x040048D1 RID: 18641
		public static List<MeiMaoYanSeRandomColorJsonData> DataList = new List<MeiMaoYanSeRandomColorJsonData>();

		// Token: 0x040048D2 RID: 18642
		public static Action OnInitFinishAction = new Action(MeiMaoYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x040048D3 RID: 18643
		public int id;

		// Token: 0x040048D4 RID: 18644
		public int R;

		// Token: 0x040048D5 RID: 18645
		public int G;

		// Token: 0x040048D6 RID: 18646
		public int B;

		// Token: 0x040048D7 RID: 18647
		public string beizhu;
	}
}
