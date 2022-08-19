using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F3 RID: 2291
	public class SkillSeidJsonData118 : IJSONClass
	{
		// Token: 0x060041DF RID: 16863 RVA: 0x001C29A0 File Offset: 0x001C0BA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[118].list)
			{
				try
				{
					SkillSeidJsonData118 skillSeidJsonData = new SkillSeidJsonData118();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData118.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData118.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData118.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData118.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData118.OnInitFinishAction != null)
			{
				SkillSeidJsonData118.OnInitFinishAction();
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400417D RID: 16765
		public static int SEIDID = 118;

		// Token: 0x0400417E RID: 16766
		public static Dictionary<int, SkillSeidJsonData118> DataDict = new Dictionary<int, SkillSeidJsonData118>();

		// Token: 0x0400417F RID: 16767
		public static List<SkillSeidJsonData118> DataList = new List<SkillSeidJsonData118>();

		// Token: 0x04004180 RID: 16768
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData118.OnInitFinish);

		// Token: 0x04004181 RID: 16769
		public int id;

		// Token: 0x04004182 RID: 16770
		public List<int> value1 = new List<int>();

		// Token: 0x04004183 RID: 16771
		public List<int> value2 = new List<int>();
	}
}
