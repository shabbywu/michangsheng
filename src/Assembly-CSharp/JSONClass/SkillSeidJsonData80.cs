using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094B RID: 2379
	public class SkillSeidJsonData80 : IJSONClass
	{
		// Token: 0x0600433E RID: 17214 RVA: 0x001CA7EC File Offset: 0x001C89EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[80].list)
			{
				try
				{
					SkillSeidJsonData80 skillSeidJsonData = new SkillSeidJsonData80();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData80.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData80.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData80.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData80.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData80.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData80.OnInitFinishAction != null)
			{
				SkillSeidJsonData80.OnInitFinishAction();
			}
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043D2 RID: 17362
		public static int SEIDID = 80;

		// Token: 0x040043D3 RID: 17363
		public static Dictionary<int, SkillSeidJsonData80> DataDict = new Dictionary<int, SkillSeidJsonData80>();

		// Token: 0x040043D4 RID: 17364
		public static List<SkillSeidJsonData80> DataList = new List<SkillSeidJsonData80>();

		// Token: 0x040043D5 RID: 17365
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData80.OnInitFinish);

		// Token: 0x040043D6 RID: 17366
		public int skillid;

		// Token: 0x040043D7 RID: 17367
		public List<int> value1 = new List<int>();

		// Token: 0x040043D8 RID: 17368
		public List<int> value2 = new List<int>();
	}
}
