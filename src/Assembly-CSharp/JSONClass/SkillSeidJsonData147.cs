using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000904 RID: 2308
	public class SkillSeidJsonData147 : IJSONClass
	{
		// Token: 0x06004223 RID: 16931 RVA: 0x001C4174 File Offset: 0x001C2374
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

		// Token: 0x06004224 RID: 16932 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041E9 RID: 16873
		public static int SEIDID = 147;

		// Token: 0x040041EA RID: 16874
		public static Dictionary<int, SkillSeidJsonData147> DataDict = new Dictionary<int, SkillSeidJsonData147>();

		// Token: 0x040041EB RID: 16875
		public static List<SkillSeidJsonData147> DataList = new List<SkillSeidJsonData147>();

		// Token: 0x040041EC RID: 16876
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData147.OnInitFinish);

		// Token: 0x040041ED RID: 16877
		public int skillid;

		// Token: 0x040041EE RID: 16878
		public int target;

		// Token: 0x040041EF RID: 16879
		public int value1;

		// Token: 0x040041F0 RID: 16880
		public int value2;

		// Token: 0x040041F1 RID: 16881
		public string panduan;
	}
}
