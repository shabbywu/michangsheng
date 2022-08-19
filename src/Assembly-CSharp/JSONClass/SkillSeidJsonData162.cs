using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000910 RID: 2320
	public class SkillSeidJsonData162 : IJSONClass
	{
		// Token: 0x06004252 RID: 16978 RVA: 0x001C53FC File Offset: 0x001C35FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[162].list)
			{
				try
				{
					SkillSeidJsonData162 skillSeidJsonData = new SkillSeidJsonData162();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData162.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData162.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData162.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData162.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData162.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData162.OnInitFinishAction != null)
			{
				SkillSeidJsonData162.OnInitFinishAction();
			}
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004247 RID: 16967
		public static int SEIDID = 162;

		// Token: 0x04004248 RID: 16968
		public static Dictionary<int, SkillSeidJsonData162> DataDict = new Dictionary<int, SkillSeidJsonData162>();

		// Token: 0x04004249 RID: 16969
		public static List<SkillSeidJsonData162> DataList = new List<SkillSeidJsonData162>();

		// Token: 0x0400424A RID: 16970
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData162.OnInitFinish);

		// Token: 0x0400424B RID: 16971
		public int id;

		// Token: 0x0400424C RID: 16972
		public int target;

		// Token: 0x0400424D RID: 16973
		public int value1;

		// Token: 0x0400424E RID: 16974
		public int value2;
	}
}
