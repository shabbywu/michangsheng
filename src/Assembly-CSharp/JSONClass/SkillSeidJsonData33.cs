using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000920 RID: 2336
	public class SkillSeidJsonData33 : IJSONClass
	{
		// Token: 0x06004292 RID: 17042 RVA: 0x001C6AA0 File Offset: 0x001C4CA0
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

		// Token: 0x06004293 RID: 17043 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042B0 RID: 17072
		public static int SEIDID = 33;

		// Token: 0x040042B1 RID: 17073
		public static Dictionary<int, SkillSeidJsonData33> DataDict = new Dictionary<int, SkillSeidJsonData33>();

		// Token: 0x040042B2 RID: 17074
		public static List<SkillSeidJsonData33> DataList = new List<SkillSeidJsonData33>();

		// Token: 0x040042B3 RID: 17075
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData33.OnInitFinish);

		// Token: 0x040042B4 RID: 17076
		public int skillid;

		// Token: 0x040042B5 RID: 17077
		public int value1;

		// Token: 0x040042B6 RID: 17078
		public int value2;
	}
}
