using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088F RID: 2191
	public class MianWenYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x0600404F RID: 16463 RVA: 0x001B7174 File Offset: 0x001B5374
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MianWenYanSeRandomColorJsonData.list)
			{
				try
				{
					MianWenYanSeRandomColorJsonData mianWenYanSeRandomColorJsonData = new MianWenYanSeRandomColorJsonData();
					mianWenYanSeRandomColorJsonData.id = jsonobject["id"].I;
					mianWenYanSeRandomColorJsonData.R = jsonobject["R"].I;
					mianWenYanSeRandomColorJsonData.G = jsonobject["G"].I;
					mianWenYanSeRandomColorJsonData.B = jsonobject["B"].I;
					mianWenYanSeRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (MianWenYanSeRandomColorJsonData.DataDict.ContainsKey(mianWenYanSeRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MianWenYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", mianWenYanSeRandomColorJsonData.id));
					}
					else
					{
						MianWenYanSeRandomColorJsonData.DataDict.Add(mianWenYanSeRandomColorJsonData.id, mianWenYanSeRandomColorJsonData);
						MianWenYanSeRandomColorJsonData.DataList.Add(mianWenYanSeRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MianWenYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MianWenYanSeRandomColorJsonData.OnInitFinishAction != null)
			{
				MianWenYanSeRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D9C RID: 15772
		public static Dictionary<int, MianWenYanSeRandomColorJsonData> DataDict = new Dictionary<int, MianWenYanSeRandomColorJsonData>();

		// Token: 0x04003D9D RID: 15773
		public static List<MianWenYanSeRandomColorJsonData> DataList = new List<MianWenYanSeRandomColorJsonData>();

		// Token: 0x04003D9E RID: 15774
		public static Action OnInitFinishAction = new Action(MianWenYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x04003D9F RID: 15775
		public int id;

		// Token: 0x04003DA0 RID: 15776
		public int R;

		// Token: 0x04003DA1 RID: 15777
		public int G;

		// Token: 0x04003DA2 RID: 15778
		public int B;

		// Token: 0x04003DA3 RID: 15779
		public string beizhu;
	}
}
