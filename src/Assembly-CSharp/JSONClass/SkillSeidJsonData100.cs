using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E3 RID: 2275
	public class SkillSeidJsonData100 : IJSONClass
	{
		// Token: 0x0600419F RID: 16799 RVA: 0x001C1240 File Offset: 0x001BF440
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[100].list)
			{
				try
				{
					SkillSeidJsonData100 skillSeidJsonData = new SkillSeidJsonData100();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData100.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData100.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData100.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData100.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData100.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData100.OnInitFinishAction != null)
			{
				SkillSeidJsonData100.OnInitFinishAction();
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400410E RID: 16654
		public static int SEIDID = 100;

		// Token: 0x0400410F RID: 16655
		public static Dictionary<int, SkillSeidJsonData100> DataDict = new Dictionary<int, SkillSeidJsonData100>();

		// Token: 0x04004110 RID: 16656
		public static List<SkillSeidJsonData100> DataList = new List<SkillSeidJsonData100>();

		// Token: 0x04004111 RID: 16657
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData100.OnInitFinish);

		// Token: 0x04004112 RID: 16658
		public int skillid;

		// Token: 0x04004113 RID: 16659
		public int value1;

		// Token: 0x04004114 RID: 16660
		public int value2;

		// Token: 0x04004115 RID: 16661
		public string panduan;
	}
}
