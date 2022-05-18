using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA4 RID: 2980
	public class CrateAvatarSeidJsonData14 : IJSONClass
	{
		// Token: 0x060049F8 RID: 18936 RVA: 0x001F5728 File Offset: 0x001F3928
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[14].list)
			{
				try
				{
					CrateAvatarSeidJsonData14 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData14();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData14.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData14.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData14.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData14.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData14.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData14.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData14.OnInitFinishAction();
			}
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004500 RID: 17664
		public static int SEIDID = 14;

		// Token: 0x04004501 RID: 17665
		public static Dictionary<int, CrateAvatarSeidJsonData14> DataDict = new Dictionary<int, CrateAvatarSeidJsonData14>();

		// Token: 0x04004502 RID: 17666
		public static List<CrateAvatarSeidJsonData14> DataList = new List<CrateAvatarSeidJsonData14>();

		// Token: 0x04004503 RID: 17667
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData14.OnInitFinish);

		// Token: 0x04004504 RID: 17668
		public int id;

		// Token: 0x04004505 RID: 17669
		public int value1;
	}
}
