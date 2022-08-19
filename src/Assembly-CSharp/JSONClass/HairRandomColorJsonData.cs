using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000845 RID: 2117
	public class HairRandomColorJsonData : IJSONClass
	{
		// Token: 0x06003F26 RID: 16166 RVA: 0x001AF7E8 File Offset: 0x001AD9E8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.HairRandomColorJsonData.list)
			{
				try
				{
					HairRandomColorJsonData hairRandomColorJsonData = new HairRandomColorJsonData();
					hairRandomColorJsonData.id = jsonobject["id"].I;
					hairRandomColorJsonData.R = jsonobject["R"].I;
					hairRandomColorJsonData.G = jsonobject["G"].I;
					hairRandomColorJsonData.B = jsonobject["B"].I;
					hairRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (HairRandomColorJsonData.DataDict.ContainsKey(hairRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典HairRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", hairRandomColorJsonData.id));
					}
					else
					{
						HairRandomColorJsonData.DataDict.Add(hairRandomColorJsonData.id, hairRandomColorJsonData);
						HairRandomColorJsonData.DataList.Add(hairRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典HairRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (HairRandomColorJsonData.OnInitFinishAction != null)
			{
				HairRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B2C RID: 15148
		public static Dictionary<int, HairRandomColorJsonData> DataDict = new Dictionary<int, HairRandomColorJsonData>();

		// Token: 0x04003B2D RID: 15149
		public static List<HairRandomColorJsonData> DataList = new List<HairRandomColorJsonData>();

		// Token: 0x04003B2E RID: 15150
		public static Action OnInitFinishAction = new Action(HairRandomColorJsonData.OnInitFinish);

		// Token: 0x04003B2F RID: 15151
		public int id;

		// Token: 0x04003B30 RID: 15152
		public int R;

		// Token: 0x04003B31 RID: 15153
		public int G;

		// Token: 0x04003B32 RID: 15154
		public int B;

		// Token: 0x04003B33 RID: 15155
		public string beizhu;
	}
}
