using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200087B RID: 2171
	public class LianQiShuXinLeiBie : IJSONClass
	{
		// Token: 0x06003FFF RID: 16383 RVA: 0x001B4F84 File Offset: 0x001B3184
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

		// Token: 0x06004000 RID: 16384 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003CEB RID: 15595
		public static Dictionary<int, LianQiShuXinLeiBie> DataDict = new Dictionary<int, LianQiShuXinLeiBie>();

		// Token: 0x04003CEC RID: 15596
		public static List<LianQiShuXinLeiBie> DataList = new List<LianQiShuXinLeiBie>();

		// Token: 0x04003CED RID: 15597
		public static Action OnInitFinishAction = new Action(LianQiShuXinLeiBie.OnInitFinish);

		// Token: 0x04003CEE RID: 15598
		public int id;

		// Token: 0x04003CEF RID: 15599
		public int AttackType;

		// Token: 0x04003CF0 RID: 15600
		public string desc;
	}
}
