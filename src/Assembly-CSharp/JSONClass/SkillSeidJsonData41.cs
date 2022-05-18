using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAD RID: 3245
	public class SkillSeidJsonData41 : IJSONClass
	{
		// Token: 0x06004E1C RID: 19996 RVA: 0x0020DF6C File Offset: 0x0020C16C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[41].list)
			{
				try
				{
					SkillSeidJsonData41 skillSeidJsonData = new SkillSeidJsonData41();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData41.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData41.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData41.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData41.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData41.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData41.OnInitFinishAction != null)
			{
				SkillSeidJsonData41.OnInitFinishAction();
			}
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DFB RID: 19963
		public static int SEIDID = 41;

		// Token: 0x04004DFC RID: 19964
		public static Dictionary<int, SkillSeidJsonData41> DataDict = new Dictionary<int, SkillSeidJsonData41>();

		// Token: 0x04004DFD RID: 19965
		public static List<SkillSeidJsonData41> DataList = new List<SkillSeidJsonData41>();

		// Token: 0x04004DFE RID: 19966
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData41.OnInitFinish);

		// Token: 0x04004DFF RID: 19967
		public int skillid;

		// Token: 0x04004E00 RID: 19968
		public int value1;

		// Token: 0x04004E01 RID: 19969
		public int value2;
	}
}
