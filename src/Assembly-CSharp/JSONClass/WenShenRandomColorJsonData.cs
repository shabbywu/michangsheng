using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFD RID: 3325
	public class WenShenRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004F5E RID: 20318 RVA: 0x00214C88 File Offset: 0x00212E88
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WenShenRandomColorJsonData.list)
			{
				try
				{
					WenShenRandomColorJsonData wenShenRandomColorJsonData = new WenShenRandomColorJsonData();
					wenShenRandomColorJsonData.id = jsonobject["id"].I;
					wenShenRandomColorJsonData.R = jsonobject["R"].I;
					wenShenRandomColorJsonData.G = jsonobject["G"].I;
					wenShenRandomColorJsonData.B = jsonobject["B"].I;
					wenShenRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (WenShenRandomColorJsonData.DataDict.ContainsKey(wenShenRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wenShenRandomColorJsonData.id));
					}
					else
					{
						WenShenRandomColorJsonData.DataDict.Add(wenShenRandomColorJsonData.id, wenShenRandomColorJsonData);
						WenShenRandomColorJsonData.DataList.Add(wenShenRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WenShenRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WenShenRandomColorJsonData.OnInitFinishAction != null)
			{
				WenShenRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005067 RID: 20583
		public static Dictionary<int, WenShenRandomColorJsonData> DataDict = new Dictionary<int, WenShenRandomColorJsonData>();

		// Token: 0x04005068 RID: 20584
		public static List<WenShenRandomColorJsonData> DataList = new List<WenShenRandomColorJsonData>();

		// Token: 0x04005069 RID: 20585
		public static Action OnInitFinishAction = new Action(WenShenRandomColorJsonData.OnInitFinish);

		// Token: 0x0400506A RID: 20586
		public int id;

		// Token: 0x0400506B RID: 20587
		public int R;

		// Token: 0x0400506C RID: 20588
		public int G;

		// Token: 0x0400506D RID: 20589
		public int B;

		// Token: 0x0400506E RID: 20590
		public string beizhu;
	}
}
