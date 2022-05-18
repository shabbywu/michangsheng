using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA4 RID: 3236
	public class SkillSeidJsonData33 : IJSONClass
	{
		// Token: 0x06004DF8 RID: 19960 RVA: 0x0020D4A0 File Offset: 0x0020B6A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[33].list)
			{
				try
				{
					SkillSeidJsonData33 skillSeidJsonData = new SkillSeidJsonData33();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData33.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData33.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData33.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData33.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData33.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData33.OnInitFinishAction != null)
			{
				SkillSeidJsonData33.OnInitFinishAction();
			}
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DC0 RID: 19904
		public static int SEIDID = 33;

		// Token: 0x04004DC1 RID: 19905
		public static Dictionary<int, SkillSeidJsonData33> DataDict = new Dictionary<int, SkillSeidJsonData33>();

		// Token: 0x04004DC2 RID: 19906
		public static List<SkillSeidJsonData33> DataList = new List<SkillSeidJsonData33>();

		// Token: 0x04004DC3 RID: 19907
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData33.OnInitFinish);

		// Token: 0x04004DC4 RID: 19908
		public int skillid;

		// Token: 0x04004DC5 RID: 19909
		public int value1;

		// Token: 0x04004DC6 RID: 19910
		public int value2;
	}
}
