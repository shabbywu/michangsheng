using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7E RID: 3198
	public class SkillSeidJsonData116 : IJSONClass
	{
		// Token: 0x06004D61 RID: 19809 RVA: 0x0020A57C File Offset: 0x0020877C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[116].list)
			{
				try
				{
					SkillSeidJsonData116 skillSeidJsonData = new SkillSeidJsonData116();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData116.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData116.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData116.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData116.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData116.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData116.OnInitFinishAction != null)
			{
				SkillSeidJsonData116.OnInitFinishAction();
			}
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CB9 RID: 19641
		public static int SEIDID = 116;

		// Token: 0x04004CBA RID: 19642
		public static Dictionary<int, SkillSeidJsonData116> DataDict = new Dictionary<int, SkillSeidJsonData116>();

		// Token: 0x04004CBB RID: 19643
		public static List<SkillSeidJsonData116> DataList = new List<SkillSeidJsonData116>();

		// Token: 0x04004CBC RID: 19644
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData116.OnInitFinish);

		// Token: 0x04004CBD RID: 19645
		public int skillid;

		// Token: 0x04004CBE RID: 19646
		public int value1;

		// Token: 0x04004CBF RID: 19647
		public int value2;

		// Token: 0x04004CC0 RID: 19648
		public int value3;
	}
}
