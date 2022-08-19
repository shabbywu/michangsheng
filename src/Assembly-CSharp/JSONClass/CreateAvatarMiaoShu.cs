using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081F RID: 2079
	public class CreateAvatarMiaoShu : IJSONClass
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x001AB82C File Offset: 0x001A9A2C
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

		// Token: 0x06003E8F RID: 16015 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039EA RID: 14826
		public static Dictionary<int, CreateAvatarMiaoShu> DataDict = new Dictionary<int, CreateAvatarMiaoShu>();

		// Token: 0x040039EB RID: 14827
		public static List<CreateAvatarMiaoShu> DataList = new List<CreateAvatarMiaoShu>();

		// Token: 0x040039EC RID: 14828
		public static Action OnInitFinishAction = new Action(CreateAvatarMiaoShu.OnInitFinish);

		// Token: 0x040039ED RID: 14829
		public int id;

		// Token: 0x040039EE RID: 14830
		public string title;

		// Token: 0x040039EF RID: 14831
		public string Info;
	}
}
