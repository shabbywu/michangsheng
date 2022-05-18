using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C71 RID: 3185
	public class SkillSeidJsonData101 : IJSONClass
	{
		// Token: 0x06004D2D RID: 19757 RVA: 0x00209574 File Offset: 0x00207774
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[101].list)
			{
				try
				{
					SkillSeidJsonData101 skillSeidJsonData = new SkillSeidJsonData101();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData101.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData101.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData101.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData101.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData101.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData101.OnInitFinishAction != null)
			{
				SkillSeidJsonData101.OnInitFinishAction();
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C60 RID: 19552
		public static int SEIDID = 101;

		// Token: 0x04004C61 RID: 19553
		public static Dictionary<int, SkillSeidJsonData101> DataDict = new Dictionary<int, SkillSeidJsonData101>();

		// Token: 0x04004C62 RID: 19554
		public static List<SkillSeidJsonData101> DataList = new List<SkillSeidJsonData101>();

		// Token: 0x04004C63 RID: 19555
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData101.OnInitFinish);

		// Token: 0x04004C64 RID: 19556
		public int skillid;

		// Token: 0x04004C65 RID: 19557
		public int value1;
	}
}
