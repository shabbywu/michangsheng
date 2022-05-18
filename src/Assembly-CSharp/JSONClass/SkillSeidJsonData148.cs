using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8C RID: 3212
	public class SkillSeidJsonData148 : IJSONClass
	{
		// Token: 0x06004D99 RID: 19865 RVA: 0x0020B678 File Offset: 0x00209878
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[148].list)
			{
				try
				{
					SkillSeidJsonData148 skillSeidJsonData = new SkillSeidJsonData148();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (SkillSeidJsonData148.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData148.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData148.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData148.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData148.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData148.OnInitFinishAction != null)
			{
				SkillSeidJsonData148.OnInitFinishAction();
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D14 RID: 19732
		public static int SEIDID = 148;

		// Token: 0x04004D15 RID: 19733
		public static Dictionary<int, SkillSeidJsonData148> DataDict = new Dictionary<int, SkillSeidJsonData148>();

		// Token: 0x04004D16 RID: 19734
		public static List<SkillSeidJsonData148> DataList = new List<SkillSeidJsonData148>();

		// Token: 0x04004D17 RID: 19735
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData148.OnInitFinish);

		// Token: 0x04004D18 RID: 19736
		public int skillid;

		// Token: 0x04004D19 RID: 19737
		public int target;

		// Token: 0x04004D1A RID: 19738
		public int value1;

		// Token: 0x04004D1B RID: 19739
		public int value2;

		// Token: 0x04004D1C RID: 19740
		public string panduan;
	}
}
