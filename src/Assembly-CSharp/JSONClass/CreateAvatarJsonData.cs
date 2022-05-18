using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB4 RID: 2996
	public class CreateAvatarJsonData : IJSONClass
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x001F6A28 File Offset: 0x001F4C28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CreateAvatarJsonData.list)
			{
				try
				{
					CreateAvatarJsonData createAvatarJsonData = new CreateAvatarJsonData();
					createAvatarJsonData.id = jsonobject["id"].I;
					createAvatarJsonData.fenZu = jsonobject["fenZu"].I;
					createAvatarJsonData.feiYong = jsonobject["feiYong"].I;
					createAvatarJsonData.fenLeiGuanLian = jsonobject["fenLeiGuanLian"].I;
					createAvatarJsonData.jiesuo = jsonobject["jiesuo"].I;
					createAvatarJsonData.Title = jsonobject["Title"].Str;
					createAvatarJsonData.fenLei = jsonobject["fenLei"].Str;
					createAvatarJsonData.Desc = jsonobject["Desc"].Str;
					createAvatarJsonData.Info = jsonobject["Info"].Str;
					createAvatarJsonData.seid = jsonobject["seid"].ToList();
					if (CreateAvatarJsonData.DataDict.ContainsKey(createAvatarJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", createAvatarJsonData.id));
					}
					else
					{
						CreateAvatarJsonData.DataDict.Add(createAvatarJsonData.id, createAvatarJsonData);
						CreateAvatarJsonData.DataList.Add(createAvatarJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CreateAvatarJsonData.OnInitFinishAction != null)
			{
				CreateAvatarJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004565 RID: 17765
		public static Dictionary<int, CreateAvatarJsonData> DataDict = new Dictionary<int, CreateAvatarJsonData>();

		// Token: 0x04004566 RID: 17766
		public static List<CreateAvatarJsonData> DataList = new List<CreateAvatarJsonData>();

		// Token: 0x04004567 RID: 17767
		public static Action OnInitFinishAction = new Action(CreateAvatarJsonData.OnInitFinish);

		// Token: 0x04004568 RID: 17768
		public int id;

		// Token: 0x04004569 RID: 17769
		public int fenZu;

		// Token: 0x0400456A RID: 17770
		public int feiYong;

		// Token: 0x0400456B RID: 17771
		public int fenLeiGuanLian;

		// Token: 0x0400456C RID: 17772
		public int jiesuo;

		// Token: 0x0400456D RID: 17773
		public string Title;

		// Token: 0x0400456E RID: 17774
		public string fenLei;

		// Token: 0x0400456F RID: 17775
		public string Desc;

		// Token: 0x04004570 RID: 17776
		public string Info;

		// Token: 0x04004571 RID: 17777
		public List<int> seid = new List<int>();
	}
}
