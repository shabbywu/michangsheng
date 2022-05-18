using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFA RID: 3322
	public class TianJieMiShuData : IJSONClass
	{
		// Token: 0x06004F52 RID: 20306 RVA: 0x00214770 File Offset: 0x00212970
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

		// Token: 0x06004F53 RID: 20307 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005046 RID: 20550
		public static Dictionary<string, TianJieMiShuData> DataDict = new Dictionary<string, TianJieMiShuData>();

		// Token: 0x04005047 RID: 20551
		public static List<TianJieMiShuData> DataList = new List<TianJieMiShuData>();

		// Token: 0x04005048 RID: 20552
		public static Action OnInitFinishAction = new Action(TianJieMiShuData.OnInitFinish);

		// Token: 0x04005049 RID: 20553
		public int Skill_ID;

		// Token: 0x0400504A RID: 20554
		public int Type;

		// Token: 0x0400504B RID: 20555
		public int RoundLimit;

		// Token: 0x0400504C RID: 20556
		public int StaticValueID;

		// Token: 0x0400504D RID: 20557
		public int StuTime;

		// Token: 0x0400504E RID: 20558
		public int GongBi;

		// Token: 0x0400504F RID: 20559
		public int DiYiXiang;

		// Token: 0x04005050 RID: 20560
		public int XiuZhengZhi;

		// Token: 0x04005051 RID: 20561
		public string id;

		// Token: 0x04005052 RID: 20562
		public string StartFightAction;

		// Token: 0x04005053 RID: 20563
		public string PanDing;

		// Token: 0x04005054 RID: 20564
		public string desc;

		// Token: 0x04005055 RID: 20565
		public string ShuoMing;
	}
}
