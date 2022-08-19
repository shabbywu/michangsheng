using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000916 RID: 2326
	public class SkillSeidJsonData174 : IJSONClass
	{
		// Token: 0x0600426A RID: 17002 RVA: 0x001C5CA0 File Offset: 0x001C3EA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[174].list)
			{
				try
				{
					SkillSeidJsonData174 skillSeidJsonData = new SkillSeidJsonData174();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData174.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData174.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData174.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData174.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData174.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData174.OnInitFinishAction != null)
			{
				SkillSeidJsonData174.OnInitFinishAction();
			}
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004270 RID: 17008
		public static int SEIDID = 174;

		// Token: 0x04004271 RID: 17009
		public static Dictionary<int, SkillSeidJsonData174> DataDict = new Dictionary<int, SkillSeidJsonData174>();

		// Token: 0x04004272 RID: 17010
		public static List<SkillSeidJsonData174> DataList = new List<SkillSeidJsonData174>();

		// Token: 0x04004273 RID: 17011
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData174.OnInitFinish);

		// Token: 0x04004274 RID: 17012
		public int skillid;

		// Token: 0x04004275 RID: 17013
		public int value1;
	}
}
