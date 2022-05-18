using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CEB RID: 3307
	public class StaticSkillSeidJsonData7 : IJSONClass
	{
		// Token: 0x06004F14 RID: 20244 RVA: 0x00212C40 File Offset: 0x00210E40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[7].list)
			{
				try
				{
					StaticSkillSeidJsonData7 staticSkillSeidJsonData = new StaticSkillSeidJsonData7();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData7.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData7.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData7.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData7.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FA7 RID: 20391
		public static int SEIDID = 7;

		// Token: 0x04004FA8 RID: 20392
		public static Dictionary<int, StaticSkillSeidJsonData7> DataDict = new Dictionary<int, StaticSkillSeidJsonData7>();

		// Token: 0x04004FA9 RID: 20393
		public static List<StaticSkillSeidJsonData7> DataList = new List<StaticSkillSeidJsonData7>();

		// Token: 0x04004FAA RID: 20394
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData7.OnInitFinish);

		// Token: 0x04004FAB RID: 20395
		public int skillid;

		// Token: 0x04004FAC RID: 20396
		public int value1;
	}
}
