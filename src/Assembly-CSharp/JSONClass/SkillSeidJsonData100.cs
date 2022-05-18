using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C70 RID: 3184
	public class SkillSeidJsonData100 : IJSONClass
	{
		// Token: 0x06004D29 RID: 19753 RVA: 0x00209420 File Offset: 0x00207620
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

		// Token: 0x06004D2A RID: 19754 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C58 RID: 19544
		public static int SEIDID = 100;

		// Token: 0x04004C59 RID: 19545
		public static Dictionary<int, SkillSeidJsonData100> DataDict = new Dictionary<int, SkillSeidJsonData100>();

		// Token: 0x04004C5A RID: 19546
		public static List<SkillSeidJsonData100> DataList = new List<SkillSeidJsonData100>();

		// Token: 0x04004C5B RID: 19547
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData100.OnInitFinish);

		// Token: 0x04004C5C RID: 19548
		public int skillid;

		// Token: 0x04004C5D RID: 19549
		public int value1;

		// Token: 0x04004C5E RID: 19550
		public int value2;

		// Token: 0x04004C5F RID: 19551
		public string panduan;
	}
}
