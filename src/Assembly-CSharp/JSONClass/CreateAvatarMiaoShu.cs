using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB5 RID: 2997
	public class CreateAvatarMiaoShu : IJSONClass
	{
		// Token: 0x06004A3C RID: 19004 RVA: 0x001F6C14 File Offset: 0x001F4E14
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CreateAvatarMiaoShu.list)
			{
				try
				{
					CreateAvatarMiaoShu createAvatarMiaoShu = new CreateAvatarMiaoShu();
					createAvatarMiaoShu.id = jsonobject["id"].I;
					createAvatarMiaoShu.title = jsonobject["title"].Str;
					createAvatarMiaoShu.Info = jsonobject["Info"].Str;
					if (CreateAvatarMiaoShu.DataDict.ContainsKey(createAvatarMiaoShu.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CreateAvatarMiaoShu.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", createAvatarMiaoShu.id));
					}
					else
					{
						CreateAvatarMiaoShu.DataDict.Add(createAvatarMiaoShu.id, createAvatarMiaoShu);
						CreateAvatarMiaoShu.DataList.Add(createAvatarMiaoShu);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CreateAvatarMiaoShu.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CreateAvatarMiaoShu.OnInitFinishAction != null)
			{
				CreateAvatarMiaoShu.OnInitFinishAction();
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004572 RID: 17778
		public static Dictionary<int, CreateAvatarMiaoShu> DataDict = new Dictionary<int, CreateAvatarMiaoShu>();

		// Token: 0x04004573 RID: 17779
		public static List<CreateAvatarMiaoShu> DataList = new List<CreateAvatarMiaoShu>();

		// Token: 0x04004574 RID: 17780
		public static Action OnInitFinishAction = new Action(CreateAvatarMiaoShu.OnInitFinish);

		// Token: 0x04004575 RID: 17781
		public int id;

		// Token: 0x04004576 RID: 17782
		public string title;

		// Token: 0x04004577 RID: 17783
		public string Info;
	}
}
