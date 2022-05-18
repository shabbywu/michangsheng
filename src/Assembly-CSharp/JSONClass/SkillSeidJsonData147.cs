using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8B RID: 3211
	public class SkillSeidJsonData147 : IJSONClass
	{
		// Token: 0x06004D95 RID: 19861 RVA: 0x0020B4F4 File Offset: 0x002096F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[147].list)
			{
				try
				{
					SkillSeidJsonData147 skillSeidJsonData = new SkillSeidJsonData147();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData147.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData147.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData147.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData147.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData147.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData147.OnInitFinishAction != null)
			{
				SkillSeidJsonData147.OnInitFinishAction();
			}
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D0B RID: 19723
		public static int SEIDID = 147;

		// Token: 0x04004D0C RID: 19724
		public static Dictionary<int, SkillSeidJsonData147> DataDict = new Dictionary<int, SkillSeidJsonData147>();

		// Token: 0x04004D0D RID: 19725
		public static List<SkillSeidJsonData147> DataList = new List<SkillSeidJsonData147>();

		// Token: 0x04004D0E RID: 19726
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData147.OnInitFinish);

		// Token: 0x04004D0F RID: 19727
		public int skillid;

		// Token: 0x04004D10 RID: 19728
		public int target;

		// Token: 0x04004D11 RID: 19729
		public int value1;

		// Token: 0x04004D12 RID: 19730
		public int value2;

		// Token: 0x04004D13 RID: 19731
		public string panduan;
	}
}
