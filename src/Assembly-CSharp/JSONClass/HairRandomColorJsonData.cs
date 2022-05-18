using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD5 RID: 3029
	public class HairRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004ABC RID: 19132 RVA: 0x001F9D3C File Offset: 0x001F7F3C
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

		// Token: 0x06004ABD RID: 19133 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400468F RID: 18063
		public static Dictionary<int, HairRandomColorJsonData> DataDict = new Dictionary<int, HairRandomColorJsonData>();

		// Token: 0x04004690 RID: 18064
		public static List<HairRandomColorJsonData> DataList = new List<HairRandomColorJsonData>();

		// Token: 0x04004691 RID: 18065
		public static Action OnInitFinishAction = new Action(HairRandomColorJsonData.OnInitFinish);

		// Token: 0x04004692 RID: 18066
		public int id;

		// Token: 0x04004693 RID: 18067
		public int R;

		// Token: 0x04004694 RID: 18068
		public int G;

		// Token: 0x04004695 RID: 18069
		public int B;

		// Token: 0x04004696 RID: 18070
		public string beizhu;
	}
}
