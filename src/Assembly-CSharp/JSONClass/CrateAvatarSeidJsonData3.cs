using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BAD RID: 2989
	public class CrateAvatarSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004A1C RID: 18972 RVA: 0x001F6210 File Offset: 0x001F4410
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[3].list)
			{
				try
				{
					CrateAvatarSeidJsonData3 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData3();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData3.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData3.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData3.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData3.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400453B RID: 17723
		public static int SEIDID = 3;

		// Token: 0x0400453C RID: 17724
		public static Dictionary<int, CrateAvatarSeidJsonData3> DataDict = new Dictionary<int, CrateAvatarSeidJsonData3>();

		// Token: 0x0400453D RID: 17725
		public static List<CrateAvatarSeidJsonData3> DataList = new List<CrateAvatarSeidJsonData3>();

		// Token: 0x0400453E RID: 17726
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData3.OnInitFinish);

		// Token: 0x0400453F RID: 17727
		public int id;

		// Token: 0x04004540 RID: 17728
		public int value1;
	}
}
