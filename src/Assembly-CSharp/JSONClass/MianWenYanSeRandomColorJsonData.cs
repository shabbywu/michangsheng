using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1D RID: 3101
	public class MianWenYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004BDD RID: 19421 RVA: 0x00200670 File Offset: 0x001FE870
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

		// Token: 0x06004BDE RID: 19422 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048F5 RID: 18677
		public static Dictionary<int, MianWenYanSeRandomColorJsonData> DataDict = new Dictionary<int, MianWenYanSeRandomColorJsonData>();

		// Token: 0x040048F6 RID: 18678
		public static List<MianWenYanSeRandomColorJsonData> DataList = new List<MianWenYanSeRandomColorJsonData>();

		// Token: 0x040048F7 RID: 18679
		public static Action OnInitFinishAction = new Action(MianWenYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x040048F8 RID: 18680
		public int id;

		// Token: 0x040048F9 RID: 18681
		public int R;

		// Token: 0x040048FA RID: 18682
		public int G;

		// Token: 0x040048FB RID: 18683
		public int B;

		// Token: 0x040048FC RID: 18684
		public string beizhu;
	}
}
