using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBA RID: 3002
	public class CyRandomTaskData : IJSONClass
	{
		// Token: 0x06004A50 RID: 19024 RVA: 0x001F74A8 File Offset: 0x001F56A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyRandomTaskData.list)
			{
				try
				{
					CyRandomTaskData cyRandomTaskData = new CyRandomTaskData();
					cyRandomTaskData.id = jsonobject["id"].I;
					cyRandomTaskData.info = jsonobject["info"].I;
					cyRandomTaskData.Type = jsonobject["Type"].I;
					cyRandomTaskData.NPCxingdong = jsonobject["NPCxingdong"].I;
					cyRandomTaskData.TaskID = jsonobject["TaskID"].I;
					cyRandomTaskData.TaskType = jsonobject["TaskType"].I;
					cyRandomTaskData.Taskvalue = jsonobject["Taskvalue"].I;
					cyRandomTaskData.XingWeiType = jsonobject["XingWeiType"].I;
					cyRandomTaskData.ItemID = jsonobject["ItemID"].I;
					cyRandomTaskData.ItemNum = jsonobject["ItemNum"].I;
					cyRandomTaskData.IsZhongYaoNPC = jsonobject["IsZhongYaoNPC"].I;
					cyRandomTaskData.IsOnly = jsonobject["IsOnly"].I;
					cyRandomTaskData.StarTime = jsonobject["StarTime"].Str;
					cyRandomTaskData.EndTime = jsonobject["EndTime"].Str;
					cyRandomTaskData.DelayTime = jsonobject["DelayTime"].ToList();
					cyRandomTaskData.valueID = jsonobject["valueID"].ToList();
					cyRandomTaskData.value = jsonobject["value"].ToList();
					cyRandomTaskData.Level = jsonobject["Level"].ToList();
					cyRandomTaskData.NPCLevel = jsonobject["NPCLevel"].ToList();
					cyRandomTaskData.NPCXingGe = jsonobject["NPCXingGe"].ToList();
					cyRandomTaskData.NPCType = jsonobject["NPCType"].ToList();
					cyRandomTaskData.NPCLiuPai = jsonobject["NPCLiuPai"].ToList();
					cyRandomTaskData.NPCTag = jsonobject["NPCTag"].ToList();
					cyRandomTaskData.NPCXingWei = jsonobject["NPCXingWei"].ToList();
					cyRandomTaskData.NPCGuanXi = jsonobject["NPCGuanXi"].ToList();
					cyRandomTaskData.NPCGuanXiNot = jsonobject["NPCGuanXiNot"].ToList();
					cyRandomTaskData.HaoGanDu = jsonobject["HaoGanDu"].ToList();
					cyRandomTaskData.WuDaoType = jsonobject["WuDaoType"].ToList();
					cyRandomTaskData.WuDaoLevel = jsonobject["WuDaoLevel"].ToList();
					cyRandomTaskData.EventValue = jsonobject["EventValue"].ToList();
					cyRandomTaskData.fuhao = jsonobject["fuhao"].ToList();
					cyRandomTaskData.EventValueNum = jsonobject["EventValueNum"].ToList();
					if (CyRandomTaskData.DataDict.ContainsKey(cyRandomTaskData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyRandomTaskData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyRandomTaskData.id));
					}
					else
					{
						CyRandomTaskData.DataDict.Add(cyRandomTaskData.id, cyRandomTaskData);
						CyRandomTaskData.DataList.Add(cyRandomTaskData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyRandomTaskData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyRandomTaskData.OnInitFinishAction != null)
			{
				CyRandomTaskData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045A9 RID: 17833
		public static Dictionary<int, CyRandomTaskData> DataDict = new Dictionary<int, CyRandomTaskData>();

		// Token: 0x040045AA RID: 17834
		public static List<CyRandomTaskData> DataList = new List<CyRandomTaskData>();

		// Token: 0x040045AB RID: 17835
		public static Action OnInitFinishAction = new Action(CyRandomTaskData.OnInitFinish);

		// Token: 0x040045AC RID: 17836
		public int id;

		// Token: 0x040045AD RID: 17837
		public int info;

		// Token: 0x040045AE RID: 17838
		public int Type;

		// Token: 0x040045AF RID: 17839
		public int NPCxingdong;

		// Token: 0x040045B0 RID: 17840
		public int TaskID;

		// Token: 0x040045B1 RID: 17841
		public int TaskType;

		// Token: 0x040045B2 RID: 17842
		public int Taskvalue;

		// Token: 0x040045B3 RID: 17843
		public int XingWeiType;

		// Token: 0x040045B4 RID: 17844
		public int ItemID;

		// Token: 0x040045B5 RID: 17845
		public int ItemNum;

		// Token: 0x040045B6 RID: 17846
		public int IsZhongYaoNPC;

		// Token: 0x040045B7 RID: 17847
		public int IsOnly;

		// Token: 0x040045B8 RID: 17848
		public string StarTime;

		// Token: 0x040045B9 RID: 17849
		public string EndTime;

		// Token: 0x040045BA RID: 17850
		public List<int> DelayTime = new List<int>();

		// Token: 0x040045BB RID: 17851
		public List<int> valueID = new List<int>();

		// Token: 0x040045BC RID: 17852
		public List<int> value = new List<int>();

		// Token: 0x040045BD RID: 17853
		public List<int> Level = new List<int>();

		// Token: 0x040045BE RID: 17854
		public List<int> NPCLevel = new List<int>();

		// Token: 0x040045BF RID: 17855
		public List<int> NPCXingGe = new List<int>();

		// Token: 0x040045C0 RID: 17856
		public List<int> NPCType = new List<int>();

		// Token: 0x040045C1 RID: 17857
		public List<int> NPCLiuPai = new List<int>();

		// Token: 0x040045C2 RID: 17858
		public List<int> NPCTag = new List<int>();

		// Token: 0x040045C3 RID: 17859
		public List<int> NPCXingWei = new List<int>();

		// Token: 0x040045C4 RID: 17860
		public List<int> NPCGuanXi = new List<int>();

		// Token: 0x040045C5 RID: 17861
		public List<int> NPCGuanXiNot = new List<int>();

		// Token: 0x040045C6 RID: 17862
		public List<int> HaoGanDu = new List<int>();

		// Token: 0x040045C7 RID: 17863
		public List<int> WuDaoType = new List<int>();

		// Token: 0x040045C8 RID: 17864
		public List<int> WuDaoLevel = new List<int>();

		// Token: 0x040045C9 RID: 17865
		public List<int> EventValue = new List<int>();

		// Token: 0x040045CA RID: 17866
		public List<int> fuhao = new List<int>();

		// Token: 0x040045CB RID: 17867
		public List<int> EventValueNum = new List<int>();
	}
}
