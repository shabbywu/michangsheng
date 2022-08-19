using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008EF RID: 2287
	public class SkillSeidJsonData112 : IJSONClass
	{
		// Token: 0x060041CF RID: 16847 RVA: 0x001C23B4 File Offset: 0x001C05B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[112].list)
			{
				try
				{
					SkillSeidJsonData112 skillSeidJsonData = new SkillSeidJsonData112();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData112.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData112.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData112.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData112.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData112.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData112.OnInitFinishAction != null)
			{
				SkillSeidJsonData112.OnInitFinishAction();
			}
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004161 RID: 16737
		public static int SEIDID = 112;

		// Token: 0x04004162 RID: 16738
		public static Dictionary<int, SkillSeidJsonData112> DataDict = new Dictionary<int, SkillSeidJsonData112>();

		// Token: 0x04004163 RID: 16739
		public static List<SkillSeidJsonData112> DataList = new List<SkillSeidJsonData112>();

		// Token: 0x04004164 RID: 16740
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData112.OnInitFinish);

		// Token: 0x04004165 RID: 16741
		public int skillid;

		// Token: 0x04004166 RID: 16742
		public List<int> value1 = new List<int>();

		// Token: 0x04004167 RID: 16743
		public List<int> value2 = new List<int>();
	}
}
