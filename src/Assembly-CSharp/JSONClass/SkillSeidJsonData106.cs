using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C75 RID: 3189
	public class SkillSeidJsonData106 : IJSONClass
	{
		// Token: 0x06004D3D RID: 19773 RVA: 0x00209A28 File Offset: 0x00207C28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[106].list)
			{
				try
				{
					SkillSeidJsonData106 skillSeidJsonData = new SkillSeidJsonData106();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					skillSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (SkillSeidJsonData106.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData106.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData106.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData106.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData106.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData106.OnInitFinishAction != null)
			{
				SkillSeidJsonData106.OnInitFinishAction();
			}
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C79 RID: 19577
		public static int SEIDID = 106;

		// Token: 0x04004C7A RID: 19578
		public static Dictionary<int, SkillSeidJsonData106> DataDict = new Dictionary<int, SkillSeidJsonData106>();

		// Token: 0x04004C7B RID: 19579
		public static List<SkillSeidJsonData106> DataList = new List<SkillSeidJsonData106>();

		// Token: 0x04004C7C RID: 19580
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData106.OnInitFinish);

		// Token: 0x04004C7D RID: 19581
		public int skillid;

		// Token: 0x04004C7E RID: 19582
		public List<int> value1 = new List<int>();

		// Token: 0x04004C7F RID: 19583
		public List<int> value2 = new List<int>();

		// Token: 0x04004C80 RID: 19584
		public List<int> value3 = new List<int>();

		// Token: 0x04004C81 RID: 19585
		public List<int> value4 = new List<int>();
	}
}
