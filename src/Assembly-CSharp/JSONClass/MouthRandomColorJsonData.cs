using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000890 RID: 2192
	public class MouthRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004053 RID: 16467 RVA: 0x001B731C File Offset: 0x001B551C
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

		// Token: 0x06004054 RID: 16468 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DA4 RID: 15780
		public static Dictionary<int, MouthRandomColorJsonData> DataDict = new Dictionary<int, MouthRandomColorJsonData>();

		// Token: 0x04003DA5 RID: 15781
		public static List<MouthRandomColorJsonData> DataList = new List<MouthRandomColorJsonData>();

		// Token: 0x04003DA6 RID: 15782
		public static Action OnInitFinishAction = new Action(MouthRandomColorJsonData.OnInitFinish);

		// Token: 0x04003DA7 RID: 15783
		public int id;

		// Token: 0x04003DA8 RID: 15784
		public int R;

		// Token: 0x04003DA9 RID: 15785
		public int G;

		// Token: 0x04003DAA RID: 15786
		public int B;

		// Token: 0x04003DAB RID: 15787
		public string beizhu;
	}
}
