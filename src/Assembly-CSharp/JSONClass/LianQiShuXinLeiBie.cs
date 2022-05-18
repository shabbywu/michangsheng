using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C09 RID: 3081
	public class LianQiShuXinLeiBie : IJSONClass
	{
		// Token: 0x06004B8D RID: 19341 RVA: 0x001FE7FC File Offset: 0x001FC9FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianQiShuXinLeiBie.list)
			{
				try
				{
					LianQiShuXinLeiBie lianQiShuXinLeiBie = new LianQiShuXinLeiBie();
					lianQiShuXinLeiBie.id = jsonobject["id"].I;
					lianQiShuXinLeiBie.AttackType = jsonobject["AttackType"].I;
					lianQiShuXinLeiBie.desc = jsonobject["desc"].Str;
					if (LianQiShuXinLeiBie.DataDict.ContainsKey(lianQiShuXinLeiBie.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianQiShuXinLeiBie.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianQiShuXinLeiBie.id));
					}
					else
					{
						LianQiShuXinLeiBie.DataDict.Add(lianQiShuXinLeiBie.id, lianQiShuXinLeiBie);
						LianQiShuXinLeiBie.DataList.Add(lianQiShuXinLeiBie);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianQiShuXinLeiBie.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianQiShuXinLeiBie.OnInitFinishAction != null)
			{
				LianQiShuXinLeiBie.OnInitFinishAction();
			}
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004844 RID: 18500
		public static Dictionary<int, LianQiShuXinLeiBie> DataDict = new Dictionary<int, LianQiShuXinLeiBie>();

		// Token: 0x04004845 RID: 18501
		public static List<LianQiShuXinLeiBie> DataList = new List<LianQiShuXinLeiBie>();

		// Token: 0x04004846 RID: 18502
		public static Action OnInitFinishAction = new Action(LianQiShuXinLeiBie.OnInitFinish);

		// Token: 0x04004847 RID: 18503
		public int id;

		// Token: 0x04004848 RID: 18504
		public int AttackType;

		// Token: 0x04004849 RID: 18505
		public string desc;
	}
}
