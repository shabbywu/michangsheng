using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D9 RID: 2265
	public class ShengPing : IJSONClass
	{
		// Token: 0x06004177 RID: 16759 RVA: 0x001C03E8 File Offset: 0x001BE5E8
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

		// Token: 0x06004178 RID: 16760 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040CF RID: 16591
		public static Dictionary<string, ShengPing> DataDict = new Dictionary<string, ShengPing>();

		// Token: 0x040040D0 RID: 16592
		public static List<ShengPing> DataList = new List<ShengPing>();

		// Token: 0x040040D1 RID: 16593
		public static Action OnInitFinishAction = new Action(ShengPing.OnInitFinish);

		// Token: 0x040040D2 RID: 16594
		public int IsChongfu;

		// Token: 0x040040D3 RID: 16595
		public int priority;

		// Token: 0x040040D4 RID: 16596
		public string id;

		// Token: 0x040040D5 RID: 16597
		public string descr;
	}
}
