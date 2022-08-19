using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097A RID: 2426
	public class TouXiangPianYi : IJSONClass
	{
		// Token: 0x060043FC RID: 17404 RVA: 0x001CF5C4 File Offset: 0x001CD7C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TouXiangPianYi.list)
			{
				try
				{
					TouXiangPianYi touXiangPianYi = new TouXiangPianYi();
					touXiangPianYi.PX = jsonobject["PX"].I;
					touXiangPianYi.PY = jsonobject["PY"].I;
					touXiangPianYi.SX = jsonobject["SX"].I;
					touXiangPianYi.SY = jsonobject["SY"].I;
					touXiangPianYi.id = jsonobject["id"].Str;
					if (TouXiangPianYi.DataDict.ContainsKey(touXiangPianYi.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典TouXiangPianYi.DataDict添加数据时出现重复的键，Key:" + touXiangPianYi.id + "，已跳过，请检查配表");
					}
					else
					{
						TouXiangPianYi.DataDict.Add(touXiangPianYi.id, touXiangPianYi);
						TouXiangPianYi.DataList.Add(touXiangPianYi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TouXiangPianYi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TouXiangPianYi.OnInitFinishAction != null)
			{
				TouXiangPianYi.OnInitFinishAction();
			}
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400455A RID: 17754
		public static Dictionary<string, TouXiangPianYi> DataDict = new Dictionary<string, TouXiangPianYi>();

		// Token: 0x0400455B RID: 17755
		public static List<TouXiangPianYi> DataList = new List<TouXiangPianYi>();

		// Token: 0x0400455C RID: 17756
		public static Action OnInitFinishAction = new Action(TouXiangPianYi.OnInitFinish);

		// Token: 0x0400455D RID: 17757
		public int PX;

		// Token: 0x0400455E RID: 17758
		public int PY;

		// Token: 0x0400455F RID: 17759
		public int SX;

		// Token: 0x04004560 RID: 17760
		public int SY;

		// Token: 0x04004561 RID: 17761
		public string id;
	}
}
