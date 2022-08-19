using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000927 RID: 2343
	public class SkillSeidJsonData4 : IJSONClass
	{
		// Token: 0x060042AE RID: 17070 RVA: 0x001C7444 File Offset: 0x001C5644
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[4].list)
			{
				try
				{
					SkillSeidJsonData4 skillSeidJsonData = new SkillSeidJsonData4();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData4.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData4.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData4.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData4.OnInitFinishAction != null)
			{
				SkillSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042DD RID: 17117
		public static int SEIDID = 4;

		// Token: 0x040042DE RID: 17118
		public static Dictionary<int, SkillSeidJsonData4> DataDict = new Dictionary<int, SkillSeidJsonData4>();

		// Token: 0x040042DF RID: 17119
		public static List<SkillSeidJsonData4> DataList = new List<SkillSeidJsonData4>();

		// Token: 0x040042E0 RID: 17120
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData4.OnInitFinish);

		// Token: 0x040042E1 RID: 17121
		public int skillid;

		// Token: 0x040042E2 RID: 17122
		public List<int> value1 = new List<int>();

		// Token: 0x040042E3 RID: 17123
		public List<int> value2 = new List<int>();
	}
}
