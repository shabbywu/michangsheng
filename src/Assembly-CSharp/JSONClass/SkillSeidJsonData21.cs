using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9C RID: 3228
	public class SkillSeidJsonData21 : IJSONClass
	{
		// Token: 0x06004DD8 RID: 19928 RVA: 0x0020CB10 File Offset: 0x0020AD10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[21].list)
			{
				try
				{
					SkillSeidJsonData21 skillSeidJsonData = new SkillSeidJsonData21();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData21.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData21.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData21.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData21.OnInitFinishAction != null)
			{
				SkillSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D8C RID: 19852
		public static int SEIDID = 21;

		// Token: 0x04004D8D RID: 19853
		public static Dictionary<int, SkillSeidJsonData21> DataDict = new Dictionary<int, SkillSeidJsonData21>();

		// Token: 0x04004D8E RID: 19854
		public static List<SkillSeidJsonData21> DataList = new List<SkillSeidJsonData21>();

		// Token: 0x04004D8F RID: 19855
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData21.OnInitFinish);

		// Token: 0x04004D90 RID: 19856
		public int skillid;

		// Token: 0x04004D91 RID: 19857
		public int value1;
	}
}
