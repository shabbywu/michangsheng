using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000928 RID: 2344
	public class SkillSeidJsonData40 : IJSONClass
	{
		// Token: 0x060042B2 RID: 17074 RVA: 0x001C75CC File Offset: 0x001C57CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[40].list)
			{
				try
				{
					SkillSeidJsonData40 skillSeidJsonData = new SkillSeidJsonData40();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData40.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData40.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData40.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData40.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData40.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData40.OnInitFinishAction != null)
			{
				SkillSeidJsonData40.OnInitFinishAction();
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042E4 RID: 17124
		public static int SEIDID = 40;

		// Token: 0x040042E5 RID: 17125
		public static Dictionary<int, SkillSeidJsonData40> DataDict = new Dictionary<int, SkillSeidJsonData40>();

		// Token: 0x040042E6 RID: 17126
		public static List<SkillSeidJsonData40> DataList = new List<SkillSeidJsonData40>();

		// Token: 0x040042E7 RID: 17127
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData40.OnInitFinish);

		// Token: 0x040042E8 RID: 17128
		public int skillid;

		// Token: 0x040042E9 RID: 17129
		public int value1;

		// Token: 0x040042EA RID: 17130
		public int value2;
	}
}
