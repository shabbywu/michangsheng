using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAE RID: 2990
	public class CrateAvatarSeidJsonData4 : IJSONClass
	{
		// Token: 0x06004A20 RID: 18976 RVA: 0x001F6338 File Offset: 0x001F4538
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[4].list)
			{
				try
				{
					CrateAvatarSeidJsonData4 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData4();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData4.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData4.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData4.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData4.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004541 RID: 17729
		public static int SEIDID = 4;

		// Token: 0x04004542 RID: 17730
		public static Dictionary<int, CrateAvatarSeidJsonData4> DataDict = new Dictionary<int, CrateAvatarSeidJsonData4>();

		// Token: 0x04004543 RID: 17731
		public static List<CrateAvatarSeidJsonData4> DataList = new List<CrateAvatarSeidJsonData4>();

		// Token: 0x04004544 RID: 17732
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData4.OnInitFinish);

		// Token: 0x04004545 RID: 17733
		public int id;

		// Token: 0x04004546 RID: 17734
		public int value1;
	}
}
