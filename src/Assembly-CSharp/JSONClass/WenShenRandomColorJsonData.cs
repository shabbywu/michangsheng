using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097D RID: 2429
	public class WenShenRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004408 RID: 17416 RVA: 0x001CFAAC File Offset: 0x001CDCAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WenShenRandomColorJsonData.list)
			{
				try
				{
					WenShenRandomColorJsonData wenShenRandomColorJsonData = new WenShenRandomColorJsonData();
					wenShenRandomColorJsonData.id = jsonobject["id"].I;
					wenShenRandomColorJsonData.R = jsonobject["R"].I;
					wenShenRandomColorJsonData.G = jsonobject["G"].I;
					wenShenRandomColorJsonData.B = jsonobject["B"].I;
					wenShenRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (WenShenRandomColorJsonData.DataDict.ContainsKey(wenShenRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wenShenRandomColorJsonData.id));
					}
					else
					{
						WenShenRandomColorJsonData.DataDict.Add(wenShenRandomColorJsonData.id, wenShenRandomColorJsonData);
						WenShenRandomColorJsonData.DataList.Add(wenShenRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WenShenRandomColorJsonData.OnInitFinishAction != null)
			{
				WenShenRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004573 RID: 17779
		public static Dictionary<int, WenShenRandomColorJsonData> DataDict = new Dictionary<int, WenShenRandomColorJsonData>();

		// Token: 0x04004574 RID: 17780
		public static List<WenShenRandomColorJsonData> DataList = new List<WenShenRandomColorJsonData>();

		// Token: 0x04004575 RID: 17781
		public static Action OnInitFinishAction = new Action(WenShenRandomColorJsonData.OnInitFinish);

		// Token: 0x04004576 RID: 17782
		public int id;

		// Token: 0x04004577 RID: 17783
		public int R;

		// Token: 0x04004578 RID: 17784
		public int G;

		// Token: 0x04004579 RID: 17785
		public int B;

		// Token: 0x0400457A RID: 17786
		public string beizhu;
	}
}
