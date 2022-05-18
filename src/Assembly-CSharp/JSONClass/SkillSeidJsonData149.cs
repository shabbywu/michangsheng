using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8D RID: 3213
	public class SkillSeidJsonData149 : IJSONClass
	{
		// Token: 0x06004D9D RID: 19869 RVA: 0x0020B7FC File Offset: 0x002099FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[149].list)
			{
				try
				{
					SkillSeidJsonData149 skillSeidJsonData = new SkillSeidJsonData149();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData149.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData149.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData149.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData149.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData149.OnInitFinishAction != null)
			{
				SkillSeidJsonData149.OnInitFinishAction();
			}
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D1D RID: 19741
		public static int SEIDID = 149;

		// Token: 0x04004D1E RID: 19742
		public static Dictionary<int, SkillSeidJsonData149> DataDict = new Dictionary<int, SkillSeidJsonData149>();

		// Token: 0x04004D1F RID: 19743
		public static List<SkillSeidJsonData149> DataList = new List<SkillSeidJsonData149>();

		// Token: 0x04004D20 RID: 19744
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData149.OnInitFinish);

		// Token: 0x04004D21 RID: 19745
		public int skillid;

		// Token: 0x04004D22 RID: 19746
		public int target;

		// Token: 0x04004D23 RID: 19747
		public int value1;

		// Token: 0x04004D24 RID: 19748
		public int value2;

		// Token: 0x04004D25 RID: 19749
		public string panduan;
	}
}
