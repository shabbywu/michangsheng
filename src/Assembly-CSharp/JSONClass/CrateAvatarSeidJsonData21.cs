using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAA RID: 2986
	public class CrateAvatarSeidJsonData21 : IJSONClass
	{
		// Token: 0x06004A10 RID: 18960 RVA: 0x001F5E2C File Offset: 0x001F402C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[21].list)
			{
				try
				{
					CrateAvatarSeidJsonData21 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData21();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData21.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData21.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData21.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData21.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004525 RID: 17701
		public static int SEIDID = 21;

		// Token: 0x04004526 RID: 17702
		public static Dictionary<int, CrateAvatarSeidJsonData21> DataDict = new Dictionary<int, CrateAvatarSeidJsonData21>();

		// Token: 0x04004527 RID: 17703
		public static List<CrateAvatarSeidJsonData21> DataList = new List<CrateAvatarSeidJsonData21>();

		// Token: 0x04004528 RID: 17704
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData21.OnInitFinish);

		// Token: 0x04004529 RID: 17705
		public int id;

		// Token: 0x0400452A RID: 17706
		public int value1;
	}
}
