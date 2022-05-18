using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BA6 RID: 2982
	public class CrateAvatarSeidJsonData16 : IJSONClass
	{
		// Token: 0x06004A00 RID: 18944 RVA: 0x001F598C File Offset: 0x001F3B8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CrateAvatarSeidJsonData[16].list)
			{
				try
				{
					CrateAvatarSeidJsonData16 crateAvatarSeidJsonData = new CrateAvatarSeidJsonData16();
					crateAvatarSeidJsonData.id = jsonobject["id"].I;
					crateAvatarSeidJsonData.value1 = jsonobject["value1"].I;
					if (CrateAvatarSeidJsonData16.DataDict.ContainsKey(crateAvatarSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CrateAvatarSeidJsonData16.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", crateAvatarSeidJsonData.id));
					}
					else
					{
						CrateAvatarSeidJsonData16.DataDict.Add(crateAvatarSeidJsonData.id, crateAvatarSeidJsonData);
						CrateAvatarSeidJsonData16.DataList.Add(crateAvatarSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CrateAvatarSeidJsonData16.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CrateAvatarSeidJsonData16.OnInitFinishAction != null)
			{
				CrateAvatarSeidJsonData16.OnInitFinishAction();
			}
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400450D RID: 17677
		public static int SEIDID = 16;

		// Token: 0x0400450E RID: 17678
		public static Dictionary<int, CrateAvatarSeidJsonData16> DataDict = new Dictionary<int, CrateAvatarSeidJsonData16>();

		// Token: 0x0400450F RID: 17679
		public static List<CrateAvatarSeidJsonData16> DataList = new List<CrateAvatarSeidJsonData16>();

		// Token: 0x04004510 RID: 17680
		public static Action OnInitFinishAction = new Action(CrateAvatarSeidJsonData16.OnInitFinish);

		// Token: 0x04004511 RID: 17681
		public int id;

		// Token: 0x04004512 RID: 17682
		public int value1;
	}
}
