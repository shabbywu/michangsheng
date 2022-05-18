using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C97 RID: 3223
	public class SkillSeidJsonData162 : IJSONClass
	{
		// Token: 0x06004DC4 RID: 19908 RVA: 0x0020C4D0 File Offset: 0x0020A6D0
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

		// Token: 0x06004DC5 RID: 19909 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D69 RID: 19817
		public static int SEIDID = 162;

		// Token: 0x04004D6A RID: 19818
		public static Dictionary<int, SkillSeidJsonData162> DataDict = new Dictionary<int, SkillSeidJsonData162>();

		// Token: 0x04004D6B RID: 19819
		public static List<SkillSeidJsonData162> DataList = new List<SkillSeidJsonData162>();

		// Token: 0x04004D6C RID: 19820
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData162.OnInitFinish);

		// Token: 0x04004D6D RID: 19821
		public int id;

		// Token: 0x04004D6E RID: 19822
		public int target;

		// Token: 0x04004D6F RID: 19823
		public int value1;

		// Token: 0x04004D70 RID: 19824
		public int value2;
	}
}
