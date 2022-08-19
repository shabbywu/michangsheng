using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000978 RID: 2424
	public class TianJieMiShuData : IJSONClass
	{
		// Token: 0x060043F4 RID: 17396 RVA: 0x001CF220 File Offset: 0x001CD420
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJieMiShuData.list)
			{
				try
				{
					TianJieMiShuData tianJieMiShuData = new TianJieMiShuData();
					tianJieMiShuData.Skill_ID = jsonobject["Skill_ID"].I;
					tianJieMiShuData.Type = jsonobject["Type"].I;
					tianJieMiShuData.RoundLimit = jsonobject["RoundLimit"].I;
					tianJieMiShuData.StaticValueID = jsonobject["StaticValueID"].I;
					tianJieMiShuData.StuTime = jsonobject["StuTime"].I;
					tianJieMiShuData.GongBi = jsonobject["GongBi"].I;
					tianJieMiShuData.DiYiXiang = jsonobject["DiYiXiang"].I;
					tianJieMiShuData.XiuZhengZhi = jsonobject["XiuZhengZhi"].I;
					tianJieMiShuData.id = jsonobject["id"].Str;
					tianJieMiShuData.StartFightAction = jsonobject["StartFightAction"].Str;
					tianJieMiShuData.PanDing = jsonobject["PanDing"].Str;
					tianJieMiShuData.desc = jsonobject["desc"].Str;
					tianJieMiShuData.ShuoMing = jsonobject["ShuoMing"].Str;
					if (TianJieMiShuData.DataDict.ContainsKey(tianJieMiShuData.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典TianJieMiShuData.DataDict添加数据时出现重复的键，Key:" + tianJieMiShuData.id + "，已跳过，请检查配表");
					}
					else
					{
						TianJieMiShuData.DataDict.Add(tianJieMiShuData.id, tianJieMiShuData);
						TianJieMiShuData.DataList.Add(tianJieMiShuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJieMiShuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJieMiShuData.OnInitFinishAction != null)
			{
				TianJieMiShuData.OnInitFinishAction();
			}
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004545 RID: 17733
		public static Dictionary<string, TianJieMiShuData> DataDict = new Dictionary<string, TianJieMiShuData>();

		// Token: 0x04004546 RID: 17734
		public static List<TianJieMiShuData> DataList = new List<TianJieMiShuData>();

		// Token: 0x04004547 RID: 17735
		public static Action OnInitFinishAction = new Action(TianJieMiShuData.OnInitFinish);

		// Token: 0x04004548 RID: 17736
		public int Skill_ID;

		// Token: 0x04004549 RID: 17737
		public int Type;

		// Token: 0x0400454A RID: 17738
		public int RoundLimit;

		// Token: 0x0400454B RID: 17739
		public int StaticValueID;

		// Token: 0x0400454C RID: 17740
		public int StuTime;

		// Token: 0x0400454D RID: 17741
		public int GongBi;

		// Token: 0x0400454E RID: 17742
		public int DiYiXiang;

		// Token: 0x0400454F RID: 17743
		public int XiuZhengZhi;

		// Token: 0x04004550 RID: 17744
		public string id;

		// Token: 0x04004551 RID: 17745
		public string StartFightAction;

		// Token: 0x04004552 RID: 17746
		public string PanDing;

		// Token: 0x04004553 RID: 17747
		public string desc;

		// Token: 0x04004554 RID: 17748
		public string ShuoMing;
	}
}
