using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB5 RID: 3253
	public class SkillSeidJsonData49 : IJSONClass
	{
		// Token: 0x06004E3C RID: 20028 RVA: 0x0020E9AC File Offset: 0x0020CBAC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[49].list)
			{
				try
				{
					SkillSeidJsonData49 skillSeidJsonData = new SkillSeidJsonData49();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData49.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData49.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData49.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData49.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData49.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData49.OnInitFinishAction != null)
			{
				SkillSeidJsonData49.OnInitFinishAction();
			}
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E37 RID: 20023
		public static int SEIDID = 49;

		// Token: 0x04004E38 RID: 20024
		public static Dictionary<int, SkillSeidJsonData49> DataDict = new Dictionary<int, SkillSeidJsonData49>();

		// Token: 0x04004E39 RID: 20025
		public static List<SkillSeidJsonData49> DataList = new List<SkillSeidJsonData49>();

		// Token: 0x04004E3A RID: 20026
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData49.OnInitFinish);

		// Token: 0x04004E3B RID: 20027
		public int skillid;

		// Token: 0x04004E3C RID: 20028
		public List<int> value1 = new List<int>();

		// Token: 0x04004E3D RID: 20029
		public List<int> value2 = new List<int>();
	}
}
