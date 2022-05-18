using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0F RID: 3087
	public class LingGuangJson : IJSONClass
	{
		// Token: 0x06004BA5 RID: 19365 RVA: 0x001FEF8C File Offset: 0x001FD18C
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

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004869 RID: 18537
		public static Dictionary<int, LingGuangJson> DataDict = new Dictionary<int, LingGuangJson>();

		// Token: 0x0400486A RID: 18538
		public static List<LingGuangJson> DataList = new List<LingGuangJson>();

		// Token: 0x0400486B RID: 18539
		public static Action OnInitFinishAction = new Action(LingGuangJson.OnInitFinish);

		// Token: 0x0400486C RID: 18540
		public int id;

		// Token: 0x0400486D RID: 18541
		public int type;

		// Token: 0x0400486E RID: 18542
		public int studyTime;

		// Token: 0x0400486F RID: 18543
		public int guoqiTime;

		// Token: 0x04004870 RID: 18544
		public int quality;

		// Token: 0x04004871 RID: 18545
		public string name;

		// Token: 0x04004872 RID: 18546
		public string desc;
	}
}
