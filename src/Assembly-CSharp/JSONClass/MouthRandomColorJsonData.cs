using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1E RID: 3102
	public class MouthRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004BE1 RID: 19425 RVA: 0x002007F0 File Offset: 0x001FE9F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MouthRandomColorJsonData.list)
			{
				try
				{
					MouthRandomColorJsonData mouthRandomColorJsonData = new MouthRandomColorJsonData();
					mouthRandomColorJsonData.id = jsonobject["id"].I;
					mouthRandomColorJsonData.R = jsonobject["R"].I;
					mouthRandomColorJsonData.G = jsonobject["G"].I;
					mouthRandomColorJsonData.B = jsonobject["B"].I;
					mouthRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (MouthRandomColorJsonData.DataDict.ContainsKey(mouthRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MouthRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", mouthRandomColorJsonData.id));
					}
					else
					{
						MouthRandomColorJsonData.DataDict.Add(mouthRandomColorJsonData.id, mouthRandomColorJsonData);
						MouthRandomColorJsonData.DataList.Add(mouthRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MouthRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MouthRandomColorJsonData.OnInitFinishAction != null)
			{
				MouthRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048FD RID: 18685
		public static Dictionary<int, MouthRandomColorJsonData> DataDict = new Dictionary<int, MouthRandomColorJsonData>();

		// Token: 0x040048FE RID: 18686
		public static List<MouthRandomColorJsonData> DataList = new List<MouthRandomColorJsonData>();

		// Token: 0x040048FF RID: 18687
		public static Action OnInitFinishAction = new Action(MouthRandomColorJsonData.OnInitFinish);

		// Token: 0x04004900 RID: 18688
		public int id;

		// Token: 0x04004901 RID: 18689
		public int R;

		// Token: 0x04004902 RID: 18690
		public int G;

		// Token: 0x04004903 RID: 18691
		public int B;

		// Token: 0x04004904 RID: 18692
		public string beizhu;
	}
}
