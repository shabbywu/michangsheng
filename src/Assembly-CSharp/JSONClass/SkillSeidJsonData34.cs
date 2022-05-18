using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA5 RID: 3237
	public class SkillSeidJsonData34 : IJSONClass
	{
		// Token: 0x06004DFC RID: 19964 RVA: 0x0020D5DC File Offset: 0x0020B7DC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[34].list)
			{
				try
				{
					SkillSeidJsonData34 skillSeidJsonData = new SkillSeidJsonData34();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData34.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData34.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData34.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData34.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData34.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData34.OnInitFinishAction != null)
			{
				SkillSeidJsonData34.OnInitFinishAction();
			}
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DC7 RID: 19911
		public static int SEIDID = 34;

		// Token: 0x04004DC8 RID: 19912
		public static Dictionary<int, SkillSeidJsonData34> DataDict = new Dictionary<int, SkillSeidJsonData34>();

		// Token: 0x04004DC9 RID: 19913
		public static List<SkillSeidJsonData34> DataList = new List<SkillSeidJsonData34>();

		// Token: 0x04004DCA RID: 19914
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData34.OnInitFinish);

		// Token: 0x04004DCB RID: 19915
		public int skillid;

		// Token: 0x04004DCC RID: 19916
		public int value1;
	}
}
