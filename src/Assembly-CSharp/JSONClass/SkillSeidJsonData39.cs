using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAA RID: 3242
	public class SkillSeidJsonData39 : IJSONClass
	{
		// Token: 0x06004E10 RID: 19984 RVA: 0x0020DBCC File Offset: 0x0020BDCC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[39].list)
			{
				try
				{
					SkillSeidJsonData39 skillSeidJsonData = new SkillSeidJsonData39();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData39.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData39.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData39.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData39.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData39.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData39.OnInitFinishAction != null)
			{
				SkillSeidJsonData39.OnInitFinishAction();
			}
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DE7 RID: 19943
		public static int SEIDID = 39;

		// Token: 0x04004DE8 RID: 19944
		public static Dictionary<int, SkillSeidJsonData39> DataDict = new Dictionary<int, SkillSeidJsonData39>();

		// Token: 0x04004DE9 RID: 19945
		public static List<SkillSeidJsonData39> DataList = new List<SkillSeidJsonData39>();

		// Token: 0x04004DEA RID: 19946
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData39.OnInitFinish);

		// Token: 0x04004DEB RID: 19947
		public int skillid;

		// Token: 0x04004DEC RID: 19948
		public int value1;
	}
}
