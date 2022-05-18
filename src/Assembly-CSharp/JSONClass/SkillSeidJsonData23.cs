using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9D RID: 3229
	public class SkillSeidJsonData23 : IJSONClass
	{
		// Token: 0x06004DDC RID: 19932 RVA: 0x0020CC38 File Offset: 0x0020AE38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[23].list)
			{
				try
				{
					SkillSeidJsonData23 skillSeidJsonData = new SkillSeidJsonData23();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData23.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData23.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData23.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData23.OnInitFinishAction != null)
			{
				SkillSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D92 RID: 19858
		public static int SEIDID = 23;

		// Token: 0x04004D93 RID: 19859
		public static Dictionary<int, SkillSeidJsonData23> DataDict = new Dictionary<int, SkillSeidJsonData23>();

		// Token: 0x04004D94 RID: 19860
		public static List<SkillSeidJsonData23> DataList = new List<SkillSeidJsonData23>();

		// Token: 0x04004D95 RID: 19861
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData23.OnInitFinish);

		// Token: 0x04004D96 RID: 19862
		public int skillid;

		// Token: 0x04004D97 RID: 19863
		public int value1;
	}
}
