using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCB RID: 3275
	public class SkillSeidJsonData76 : IJSONClass
	{
		// Token: 0x06004E94 RID: 20116 RVA: 0x00210458 File Offset: 0x0020E658
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[76].list)
			{
				try
				{
					SkillSeidJsonData76 skillSeidJsonData = new SkillSeidJsonData76();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData76.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData76.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData76.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData76.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData76.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData76.OnInitFinishAction != null)
			{
				SkillSeidJsonData76.OnInitFinishAction();
			}
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EC9 RID: 20169
		public static int SEIDID = 76;

		// Token: 0x04004ECA RID: 20170
		public static Dictionary<int, SkillSeidJsonData76> DataDict = new Dictionary<int, SkillSeidJsonData76>();

		// Token: 0x04004ECB RID: 20171
		public static List<SkillSeidJsonData76> DataList = new List<SkillSeidJsonData76>();

		// Token: 0x04004ECC RID: 20172
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData76.OnInitFinish);

		// Token: 0x04004ECD RID: 20173
		public int skillid;

		// Token: 0x04004ECE RID: 20174
		public int value1;
	}
}
