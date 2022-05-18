using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C66 RID: 3174
	public class ShengPing : IJSONClass
	{
		// Token: 0x06004D01 RID: 19713 RVA: 0x00208774 File Offset: 0x00206974
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ShengPing.list)
			{
				try
				{
					ShengPing shengPing = new ShengPing();
					shengPing.IsChongfu = jsonobject["IsChongfu"].I;
					shengPing.priority = jsonobject["priority"].I;
					shengPing.id = jsonobject["id"].Str;
					shengPing.descr = jsonobject["descr"].Str;
					if (ShengPing.DataDict.ContainsKey(shengPing.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典ShengPing.DataDict添加数据时出现重复的键，Key:" + shengPing.id + "，已跳过，请检查配表");
					}
					else
					{
						ShengPing.DataDict.Add(shengPing.id, shengPing);
						ShengPing.DataList.Add(shengPing);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ShengPing.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ShengPing.OnInitFinishAction != null)
			{
				ShengPing.OnInitFinishAction();
			}
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C19 RID: 19481
		public static Dictionary<string, ShengPing> DataDict = new Dictionary<string, ShengPing>();

		// Token: 0x04004C1A RID: 19482
		public static List<ShengPing> DataList = new List<ShengPing>();

		// Token: 0x04004C1B RID: 19483
		public static Action OnInitFinishAction = new Action(ShengPing.OnInitFinish);

		// Token: 0x04004C1C RID: 19484
		public int IsChongfu;

		// Token: 0x04004C1D RID: 19485
		public int priority;

		// Token: 0x04004C1E RID: 19486
		public string id;

		// Token: 0x04004C1F RID: 19487
		public string descr;
	}
}
