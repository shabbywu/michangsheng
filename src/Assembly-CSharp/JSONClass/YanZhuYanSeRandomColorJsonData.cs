using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D12 RID: 3346
	public class YanZhuYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x06004FB2 RID: 20402 RVA: 0x00216824 File Offset: 0x00214A24
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.YanZhuYanSeRandomColorJsonData.list)
			{
				try
				{
					YanZhuYanSeRandomColorJsonData yanZhuYanSeRandomColorJsonData = new YanZhuYanSeRandomColorJsonData();
					yanZhuYanSeRandomColorJsonData.id = jsonobject["id"].I;
					yanZhuYanSeRandomColorJsonData.R = jsonobject["R"].I;
					yanZhuYanSeRandomColorJsonData.G = jsonobject["G"].I;
					yanZhuYanSeRandomColorJsonData.B = jsonobject["B"].I;
					yanZhuYanSeRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (YanZhuYanSeRandomColorJsonData.DataDict.ContainsKey(yanZhuYanSeRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", yanZhuYanSeRandomColorJsonData.id));
					}
					else
					{
						YanZhuYanSeRandomColorJsonData.DataDict.Add(yanZhuYanSeRandomColorJsonData.id, yanZhuYanSeRandomColorJsonData);
						YanZhuYanSeRandomColorJsonData.DataList.Add(yanZhuYanSeRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (YanZhuYanSeRandomColorJsonData.OnInitFinishAction != null)
			{
				YanZhuYanSeRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050FB RID: 20731
		public static Dictionary<int, YanZhuYanSeRandomColorJsonData> DataDict = new Dictionary<int, YanZhuYanSeRandomColorJsonData>();

		// Token: 0x040050FC RID: 20732
		public static List<YanZhuYanSeRandomColorJsonData> DataList = new List<YanZhuYanSeRandomColorJsonData>();

		// Token: 0x040050FD RID: 20733
		public static Action OnInitFinishAction = new Action(YanZhuYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x040050FE RID: 20734
		public int id;

		// Token: 0x040050FF RID: 20735
		public int R;

		// Token: 0x04005100 RID: 20736
		public int G;

		// Token: 0x04005101 RID: 20737
		public int B;

		// Token: 0x04005102 RID: 20738
		public string beizhu;
	}
}
