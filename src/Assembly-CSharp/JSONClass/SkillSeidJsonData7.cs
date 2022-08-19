using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000943 RID: 2371
	public class SkillSeidJsonData7 : IJSONClass
	{
		// Token: 0x0600431E RID: 17182 RVA: 0x001C9C8C File Offset: 0x001C7E8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[7].list)
			{
				try
				{
					SkillSeidJsonData7 skillSeidJsonData = new SkillSeidJsonData7();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData7.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData7.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData7.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData7.OnInitFinishAction != null)
			{
				SkillSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400439D RID: 17309
		public static int SEIDID = 7;

		// Token: 0x0400439E RID: 17310
		public static Dictionary<int, SkillSeidJsonData7> DataDict = new Dictionary<int, SkillSeidJsonData7>();

		// Token: 0x0400439F RID: 17311
		public static List<SkillSeidJsonData7> DataList = new List<SkillSeidJsonData7>();

		// Token: 0x040043A0 RID: 17312
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData7.OnInitFinish);

		// Token: 0x040043A1 RID: 17313
		public int skillid;

		// Token: 0x040043A2 RID: 17314
		public List<int> value1 = new List<int>();

		// Token: 0x040043A3 RID: 17315
		public List<int> value2 = new List<int>();
	}
}
