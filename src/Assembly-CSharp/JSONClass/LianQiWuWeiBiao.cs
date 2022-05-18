using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C0A RID: 3082
	public class LianQiWuWeiBiao : IJSONClass
	{
		// Token: 0x06004B91 RID: 19345 RVA: 0x001FE938 File Offset: 0x001FCB38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiWuWeiBiao.list)
			{
				try
				{
					LianQiWuWeiBiao lianQiWuWeiBiao = new LianQiWuWeiBiao();
					lianQiWuWeiBiao.id = jsonobject["id"].I;
					lianQiWuWeiBiao.value1 = jsonobject["value1"].I;
					lianQiWuWeiBiao.value2 = jsonobject["value2"].I;
					lianQiWuWeiBiao.value3 = jsonobject["value3"].I;
					lianQiWuWeiBiao.value4 = jsonobject["value4"].I;
					lianQiWuWeiBiao.value5 = jsonobject["value5"].I;
					lianQiWuWeiBiao.desc = jsonobject["desc"].Str;
					if (LianQiWuWeiBiao.DataDict.ContainsKey(lianQiWuWeiBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiWuWeiBiao.id));
					}
					else
					{
						LianQiWuWeiBiao.DataDict.Add(lianQiWuWeiBiao.id, lianQiWuWeiBiao);
						LianQiWuWeiBiao.DataList.Add(lianQiWuWeiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiWuWeiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiWuWeiBiao.OnInitFinishAction != null)
			{
				LianQiWuWeiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400484A RID: 18506
		public static Dictionary<int, LianQiWuWeiBiao> DataDict = new Dictionary<int, LianQiWuWeiBiao>();

		// Token: 0x0400484B RID: 18507
		public static List<LianQiWuWeiBiao> DataList = new List<LianQiWuWeiBiao>();

		// Token: 0x0400484C RID: 18508
		public static Action OnInitFinishAction = new Action(LianQiWuWeiBiao.OnInitFinish);

		// Token: 0x0400484D RID: 18509
		public int id;

		// Token: 0x0400484E RID: 18510
		public int value1;

		// Token: 0x0400484F RID: 18511
		public int value2;

		// Token: 0x04004850 RID: 18512
		public int value3;

		// Token: 0x04004851 RID: 18513
		public int value4;

		// Token: 0x04004852 RID: 18514
		public int value5;

		// Token: 0x04004853 RID: 18515
		public string desc;
	}
}
