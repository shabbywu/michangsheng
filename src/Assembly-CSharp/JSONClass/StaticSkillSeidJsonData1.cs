using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE5 RID: 3301
	public class StaticSkillSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004EFC RID: 20220 RVA: 0x00212524 File Offset: 0x00210724
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[1].list)
			{
				try
				{
					StaticSkillSeidJsonData1 staticSkillSeidJsonData = new StaticSkillSeidJsonData1();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.target = jsonobject["target"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].ToList();
					staticSkillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (StaticSkillSeidJsonData1.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData1.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData1.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData1.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F81 RID: 20353
		public static int SEIDID = 1;

		// Token: 0x04004F82 RID: 20354
		public static Dictionary<int, StaticSkillSeidJsonData1> DataDict = new Dictionary<int, StaticSkillSeidJsonData1>();

		// Token: 0x04004F83 RID: 20355
		public static List<StaticSkillSeidJsonData1> DataList = new List<StaticSkillSeidJsonData1>();

		// Token: 0x04004F84 RID: 20356
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData1.OnInitFinish);

		// Token: 0x04004F85 RID: 20357
		public int skillid;

		// Token: 0x04004F86 RID: 20358
		public int target;

		// Token: 0x04004F87 RID: 20359
		public List<int> value1 = new List<int>();

		// Token: 0x04004F88 RID: 20360
		public List<int> value2 = new List<int>();
	}
}
