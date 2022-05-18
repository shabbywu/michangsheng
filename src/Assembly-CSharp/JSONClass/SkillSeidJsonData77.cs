using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CCC RID: 3276
	public class SkillSeidJsonData77 : IJSONClass
	{
		// Token: 0x06004E98 RID: 20120 RVA: 0x00210580 File Offset: 0x0020E780
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[77].list)
			{
				try
				{
					SkillSeidJsonData77 skillSeidJsonData = new SkillSeidJsonData77();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData77.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData77.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData77.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData77.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData77.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData77.OnInitFinishAction != null)
			{
				SkillSeidJsonData77.OnInitFinishAction();
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004ECF RID: 20175
		public static int SEIDID = 77;

		// Token: 0x04004ED0 RID: 20176
		public static Dictionary<int, SkillSeidJsonData77> DataDict = new Dictionary<int, SkillSeidJsonData77>();

		// Token: 0x04004ED1 RID: 20177
		public static List<SkillSeidJsonData77> DataList = new List<SkillSeidJsonData77>();

		// Token: 0x04004ED2 RID: 20178
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData77.OnInitFinish);

		// Token: 0x04004ED3 RID: 20179
		public int skillid;

		// Token: 0x04004ED4 RID: 20180
		public int value1;
	}
}
