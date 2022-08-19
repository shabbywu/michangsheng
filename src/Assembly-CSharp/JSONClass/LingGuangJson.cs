using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000881 RID: 2177
	public class LingGuangJson : IJSONClass
	{
		// Token: 0x06004017 RID: 16407 RVA: 0x001B5804 File Offset: 0x001B3A04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGuangJson.list)
			{
				try
				{
					LingGuangJson lingGuangJson = new LingGuangJson();
					lingGuangJson.id = jsonobject["id"].I;
					lingGuangJson.type = jsonobject["type"].I;
					lingGuangJson.studyTime = jsonobject["studyTime"].I;
					lingGuangJson.guoqiTime = jsonobject["guoqiTime"].I;
					lingGuangJson.quality = jsonobject["quality"].I;
					lingGuangJson.name = jsonobject["name"].Str;
					lingGuangJson.desc = jsonobject["desc"].Str;
					if (LingGuangJson.DataDict.ContainsKey(lingGuangJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingGuangJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingGuangJson.id));
					}
					else
					{
						LingGuangJson.DataDict.Add(lingGuangJson.id, lingGuangJson);
						LingGuangJson.DataList.Add(lingGuangJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingGuangJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingGuangJson.OnInitFinishAction != null)
			{
				LingGuangJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D10 RID: 15632
		public static Dictionary<int, LingGuangJson> DataDict = new Dictionary<int, LingGuangJson>();

		// Token: 0x04003D11 RID: 15633
		public static List<LingGuangJson> DataList = new List<LingGuangJson>();

		// Token: 0x04003D12 RID: 15634
		public static Action OnInitFinishAction = new Action(LingGuangJson.OnInitFinish);

		// Token: 0x04003D13 RID: 15635
		public int id;

		// Token: 0x04003D14 RID: 15636
		public int type;

		// Token: 0x04003D15 RID: 15637
		public int studyTime;

		// Token: 0x04003D16 RID: 15638
		public int guoqiTime;

		// Token: 0x04003D17 RID: 15639
		public int quality;

		// Token: 0x04003D18 RID: 15640
		public string name;

		// Token: 0x04003D19 RID: 15641
		public string desc;
	}
}
