using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB2 RID: 3250
	public class SkillSeidJsonData46 : IJSONClass
	{
		// Token: 0x06004E30 RID: 20016 RVA: 0x0020E5B0 File Offset: 0x0020C7B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[46].list)
			{
				try
				{
					SkillSeidJsonData46 skillSeidJsonData = new SkillSeidJsonData46();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData46.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData46.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData46.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData46.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData46.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData46.OnInitFinishAction != null)
			{
				SkillSeidJsonData46.OnInitFinishAction();
			}
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E1F RID: 19999
		public static int SEIDID = 46;

		// Token: 0x04004E20 RID: 20000
		public static Dictionary<int, SkillSeidJsonData46> DataDict = new Dictionary<int, SkillSeidJsonData46>();

		// Token: 0x04004E21 RID: 20001
		public static List<SkillSeidJsonData46> DataList = new List<SkillSeidJsonData46>();

		// Token: 0x04004E22 RID: 20002
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData46.OnInitFinish);

		// Token: 0x04004E23 RID: 20003
		public int skillid;

		// Token: 0x04004E24 RID: 20004
		public int value1;

		// Token: 0x04004E25 RID: 20005
		public int value2;

		// Token: 0x04004E26 RID: 20006
		public int value3;
	}
}
