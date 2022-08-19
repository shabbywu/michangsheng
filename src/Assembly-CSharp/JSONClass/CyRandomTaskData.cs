using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000824 RID: 2084
	public class CyRandomTaskData : IJSONClass
	{
		// Token: 0x06003EA2 RID: 16034 RVA: 0x001AC1C8 File Offset: 0x001AA3C8
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

		// Token: 0x06003EA3 RID: 16035 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A21 RID: 14881
		public static Dictionary<int, CyRandomTaskData> DataDict = new Dictionary<int, CyRandomTaskData>();

		// Token: 0x04003A22 RID: 14882
		public static List<CyRandomTaskData> DataList = new List<CyRandomTaskData>();

		// Token: 0x04003A23 RID: 14883
		public static Action OnInitFinishAction = new Action(CyRandomTaskData.OnInitFinish);

		// Token: 0x04003A24 RID: 14884
		public int id;

		// Token: 0x04003A25 RID: 14885
		public int info;

		// Token: 0x04003A26 RID: 14886
		public int Type;

		// Token: 0x04003A27 RID: 14887
		public int NPCxingdong;

		// Token: 0x04003A28 RID: 14888
		public int TaskID;

		// Token: 0x04003A29 RID: 14889
		public int TaskType;

		// Token: 0x04003A2A RID: 14890
		public int Taskvalue;

		// Token: 0x04003A2B RID: 14891
		public int XingWeiType;

		// Token: 0x04003A2C RID: 14892
		public int ItemID;

		// Token: 0x04003A2D RID: 14893
		public int ItemNum;

		// Token: 0x04003A2E RID: 14894
		public int IsZhongYaoNPC;

		// Token: 0x04003A2F RID: 14895
		public int IsOnly;

		// Token: 0x04003A30 RID: 14896
		public string StarTime;

		// Token: 0x04003A31 RID: 14897
		public string EndTime;

		// Token: 0x04003A32 RID: 14898
		public List<int> DelayTime = new List<int>();

		// Token: 0x04003A33 RID: 14899
		public List<int> valueID = new List<int>();

		// Token: 0x04003A34 RID: 14900
		public List<int> value = new List<int>();

		// Token: 0x04003A35 RID: 14901
		public List<int> Level = new List<int>();

		// Token: 0x04003A36 RID: 14902
		public List<int> NPCLevel = new List<int>();

		// Token: 0x04003A37 RID: 14903
		public List<int> NPCXingGe = new List<int>();

		// Token: 0x04003A38 RID: 14904
		public List<int> NPCType = new List<int>();

		// Token: 0x04003A39 RID: 14905
		public List<int> NPCLiuPai = new List<int>();

		// Token: 0x04003A3A RID: 14906
		public List<int> NPCTag = new List<int>();

		// Token: 0x04003A3B RID: 14907
		public List<int> NPCXingWei = new List<int>();

		// Token: 0x04003A3C RID: 14908
		public List<int> NPCGuanXi = new List<int>();

		// Token: 0x04003A3D RID: 14909
		public List<int> NPCGuanXiNot = new List<int>();

		// Token: 0x04003A3E RID: 14910
		public List<int> HaoGanDu = new List<int>();

		// Token: 0x04003A3F RID: 14911
		public List<int> WuDaoType = new List<int>();

		// Token: 0x04003A40 RID: 14912
		public List<int> WuDaoLevel = new List<int>();

		// Token: 0x04003A41 RID: 14913
		public List<int> EventValue = new List<int>();

		// Token: 0x04003A42 RID: 14914
		public List<int> fuhao = new List<int>();

		// Token: 0x04003A43 RID: 14915
		public List<int> EventValueNum = new List<int>();
	}
}
