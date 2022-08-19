using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094E RID: 2382
	public class SkillSeidJsonData83 : IJSONClass
	{
		// Token: 0x0600434A RID: 17226 RVA: 0x001CAC38 File Offset: 0x001C8E38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[83].list)
			{
				try
				{
					SkillSeidJsonData83 skillSeidJsonData = new SkillSeidJsonData83();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData83.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData83.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData83.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData83.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData83.OnInitFinishAction != null)
			{
				SkillSeidJsonData83.OnInitFinishAction();
			}
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043E6 RID: 17382
		public static int SEIDID = 83;

		// Token: 0x040043E7 RID: 17383
		public static Dictionary<int, SkillSeidJsonData83> DataDict = new Dictionary<int, SkillSeidJsonData83>();

		// Token: 0x040043E8 RID: 17384
		public static List<SkillSeidJsonData83> DataList = new List<SkillSeidJsonData83>();

		// Token: 0x040043E9 RID: 17385
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData83.OnInitFinish);

		// Token: 0x040043EA RID: 17386
		public int skillid;

		// Token: 0x040043EB RID: 17387
		public List<int> value1 = new List<int>();

		// Token: 0x040043EC RID: 17388
		public List<int> value2 = new List<int>();
	}
}
