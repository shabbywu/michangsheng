using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000848 RID: 2120
	public class heroFaceJsonData : IJSONClass
	{
		// Token: 0x06003F32 RID: 16178 RVA: 0x001AFC6C File Offset: 0x001ADE6C
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

		// Token: 0x06003F33 RID: 16179 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B41 RID: 15169
		public static Dictionary<int, heroFaceJsonData> DataDict = new Dictionary<int, heroFaceJsonData>();

		// Token: 0x04003B42 RID: 15170
		public static List<heroFaceJsonData> DataList = new List<heroFaceJsonData>();

		// Token: 0x04003B43 RID: 15171
		public static Action OnInitFinishAction = new Action(heroFaceJsonData.OnInitFinish);

		// Token: 0x04003B44 RID: 15172
		public int id;

		// Token: 0x04003B45 RID: 15173
		public int HeroId;

		// Token: 0x04003B46 RID: 15174
		public List<int> surfaceId = new List<int>();

		// Token: 0x04003B47 RID: 15175
		public List<int> faceMode = new List<int>();
	}
}
