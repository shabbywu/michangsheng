using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD8 RID: 3032
	public class heroFaceJsonData : IJSONClass
	{
		// Token: 0x06004AC8 RID: 19144 RVA: 0x001FA148 File Offset: 0x001F8348
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.heroFaceJsonData.list)
			{
				try
				{
					heroFaceJsonData heroFaceJsonData = new heroFaceJsonData();
					heroFaceJsonData.id = jsonobject["id"].I;
					heroFaceJsonData.HeroId = jsonobject["HeroId"].I;
					heroFaceJsonData.surfaceId = jsonobject["surfaceId"].ToList();
					heroFaceJsonData.faceMode = jsonobject["faceMode"].ToList();
					if (heroFaceJsonData.DataDict.ContainsKey(heroFaceJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典heroFaceJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", heroFaceJsonData.id));
					}
					else
					{
						heroFaceJsonData.DataDict.Add(heroFaceJsonData.id, heroFaceJsonData);
						heroFaceJsonData.DataList.Add(heroFaceJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典heroFaceJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (heroFaceJsonData.OnInitFinishAction != null)
			{
				heroFaceJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046A4 RID: 18084
		public static Dictionary<int, heroFaceJsonData> DataDict = new Dictionary<int, heroFaceJsonData>();

		// Token: 0x040046A5 RID: 18085
		public static List<heroFaceJsonData> DataList = new List<heroFaceJsonData>();

		// Token: 0x040046A6 RID: 18086
		public static Action OnInitFinishAction = new Action(heroFaceJsonData.OnInitFinish);

		// Token: 0x040046A7 RID: 18087
		public int id;

		// Token: 0x040046A8 RID: 18088
		public int HeroId;

		// Token: 0x040046A9 RID: 18089
		public List<int> surfaceId = new List<int>();

		// Token: 0x040046AA RID: 18090
		public List<int> faceMode = new List<int>();
	}
}
