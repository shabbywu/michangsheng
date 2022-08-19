using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E8 RID: 2280
	public class SkillSeidJsonData106 : IJSONClass
	{
		// Token: 0x060041B3 RID: 16819 RVA: 0x001C1954 File Offset: 0x001BFB54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[106].list)
			{
				try
				{
					SkillSeidJsonData106 skillSeidJsonData = new SkillSeidJsonData106();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					skillSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (SkillSeidJsonData106.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData106.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData106.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData106.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData106.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData106.OnInitFinishAction != null)
			{
				SkillSeidJsonData106.OnInitFinishAction();
			}
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400412F RID: 16687
		public static int SEIDID = 106;

		// Token: 0x04004130 RID: 16688
		public static Dictionary<int, SkillSeidJsonData106> DataDict = new Dictionary<int, SkillSeidJsonData106>();

		// Token: 0x04004131 RID: 16689
		public static List<SkillSeidJsonData106> DataList = new List<SkillSeidJsonData106>();

		// Token: 0x04004132 RID: 16690
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData106.OnInitFinish);

		// Token: 0x04004133 RID: 16691
		public int skillid;

		// Token: 0x04004134 RID: 16692
		public List<int> value1 = new List<int>();

		// Token: 0x04004135 RID: 16693
		public List<int> value2 = new List<int>();

		// Token: 0x04004136 RID: 16694
		public List<int> value3 = new List<int>();

		// Token: 0x04004137 RID: 16695
		public List<int> value4 = new List<int>();
	}
}
