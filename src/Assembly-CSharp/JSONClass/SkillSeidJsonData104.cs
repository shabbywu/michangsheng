using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E6 RID: 2278
	public class SkillSeidJsonData104 : IJSONClass
	{
		// Token: 0x060041AB RID: 16811 RVA: 0x001C16A4 File Offset: 0x001BF8A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[104].list)
			{
				try
				{
					SkillSeidJsonData104 skillSeidJsonData = new SkillSeidJsonData104();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData104.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData104.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData104.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData104.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData104.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData104.OnInitFinishAction != null)
			{
				SkillSeidJsonData104.OnInitFinishAction();
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004123 RID: 16675
		public static int SEIDID = 104;

		// Token: 0x04004124 RID: 16676
		public static Dictionary<int, SkillSeidJsonData104> DataDict = new Dictionary<int, SkillSeidJsonData104>();

		// Token: 0x04004125 RID: 16677
		public static List<SkillSeidJsonData104> DataList = new List<SkillSeidJsonData104>();

		// Token: 0x04004126 RID: 16678
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData104.OnInitFinish);

		// Token: 0x04004127 RID: 16679
		public int skillid;

		// Token: 0x04004128 RID: 16680
		public int value1;
	}
}
