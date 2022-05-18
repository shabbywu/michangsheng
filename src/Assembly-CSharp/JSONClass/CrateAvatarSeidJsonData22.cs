using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAB RID: 2987
	public class CrateAvatarSeidJsonData22 : IJSONClass
	{
		// Token: 0x06004A14 RID: 18964 RVA: 0x001F5F54 File Offset: 0x001F4154
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[22].list)
			{
				try
				{
					CrateAvatarSeidJsonData22 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData22();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].ToList();
					crateAvatarSeidJsonData.value2 = jsonobject["value2"].ToList();
					crateAvatarSeidJsonData.value3 = jsonobject["value3"].ToList();
					crateAvatarSeidJsonData.value4 = jsonobject["value4"].ToList();
					if (CrateAvatarSeidJsonData22.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData22.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData22.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData22.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData22.OnInitFinishAction();
			}
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400452B RID: 17707
		public static int SEIDID = 22;

		// Token: 0x0400452C RID: 17708
		public static Dictionary<int, CrateAvatarSeidJsonData22> DataDict = new Dictionary<int, CrateAvatarSeidJsonData22>();

		// Token: 0x0400452D RID: 17709
		public static List<CrateAvatarSeidJsonData22> DataList = new List<CrateAvatarSeidJsonData22>();

		// Token: 0x0400452E RID: 17710
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData22.OnInitFinish);

		// Token: 0x0400452F RID: 17711
		public int id;

		// Token: 0x04004530 RID: 17712
		public List<int> value1 = new List<int>();

		// Token: 0x04004531 RID: 17713
		public List<int> value2 = new List<int>();

		// Token: 0x04004532 RID: 17714
		public List<int> value3 = new List<int>();

		// Token: 0x04004533 RID: 17715
		public List<int> value4 = new List<int>();
	}
}
