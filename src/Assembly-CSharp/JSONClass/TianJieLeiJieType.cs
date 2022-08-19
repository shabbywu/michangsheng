using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000977 RID: 2423
	public class TianJieLeiJieType : IJSONClass
	{
		// Token: 0x060043F0 RID: 17392 RVA: 0x001CF02C File Offset: 0x001CD22C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJieLeiJieType.list)
			{
				try
				{
					TianJieLeiJieType tianJieLeiJieType = new TianJieLeiJieType();
					tianJieLeiJieType.SkillId = jsonobject["SkillId"].I;
					tianJieLeiJieType.id = jsonobject["id"].Str;
					tianJieLeiJieType.XiangXi = jsonobject["XiangXi"].Str;
					tianJieLeiJieType.CuLue = jsonobject["CuLue"].Str;
					tianJieLeiJieType.PanDing = jsonobject["PanDing"].Str;
					tianJieLeiJieType.QuanZhong = jsonobject["QuanZhong"].ToList();
					tianJieLeiJieType.QuanZhongTiSheng = jsonobject["QuanZhongTiSheng"].ToList();
					if (TianJieLeiJieType.DataDict.ContainsKey(tianJieLeiJieType.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieType.DataDict添加数据时出现重复的键，Key:" + tianJieLeiJieType.id + "，已跳过，请检查配表");
					}
					else
					{
						TianJieLeiJieType.DataDict.Add(tianJieLeiJieType.id, tianJieLeiJieType);
						TianJieLeiJieType.DataList.Add(tianJieLeiJieType);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJieLeiJieType.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJieLeiJieType.OnInitFinishAction != null)
			{
				TianJieLeiJieType.OnInitFinishAction();
			}
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400453B RID: 17723
		public static Dictionary<string, TianJieLeiJieType> DataDict = new Dictionary<string, TianJieLeiJieType>();

		// Token: 0x0400453C RID: 17724
		public static List<TianJieLeiJieType> DataList = new List<TianJieLeiJieType>();

		// Token: 0x0400453D RID: 17725
		public static Action OnInitFinishAction = new Action(TianJieLeiJieType.OnInitFinish);

		// Token: 0x0400453E RID: 17726
		public int SkillId;

		// Token: 0x0400453F RID: 17727
		public string id;

		// Token: 0x04004540 RID: 17728
		public string XiangXi;

		// Token: 0x04004541 RID: 17729
		public string CuLue;

		// Token: 0x04004542 RID: 17730
		public string PanDing;

		// Token: 0x04004543 RID: 17731
		public List<int> QuanZhong = new List<int>();

		// Token: 0x04004544 RID: 17732
		public List<int> QuanZhongTiSheng = new List<int>();
	}
}
