using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000803 RID: 2051
	public class CaiYaoDiaoLuo : IJSONClass
	{
		// Token: 0x06003E1E RID: 15902 RVA: 0x001A8CF4 File Offset: 0x001A6EF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CaiYaoDiaoLuo.list)
			{
				try
				{
					CaiYaoDiaoLuo caiYaoDiaoLuo = new CaiYaoDiaoLuo();
					caiYaoDiaoLuo.id = jsonobject["id"].I;
					caiYaoDiaoLuo.type = jsonobject["type"].I;
					caiYaoDiaoLuo.MapIndex = jsonobject["MapIndex"].I;
					caiYaoDiaoLuo.ThreeSence = jsonobject["ThreeSence"].I;
					caiYaoDiaoLuo.value1 = jsonobject["value1"].I;
					caiYaoDiaoLuo.value2 = jsonobject["value2"].I;
					caiYaoDiaoLuo.value3 = jsonobject["value3"].I;
					caiYaoDiaoLuo.value4 = jsonobject["value4"].I;
					caiYaoDiaoLuo.value5 = jsonobject["value5"].I;
					caiYaoDiaoLuo.value6 = jsonobject["value6"].I;
					caiYaoDiaoLuo.value7 = jsonobject["value7"].I;
					caiYaoDiaoLuo.value8 = jsonobject["value8"].I;
					caiYaoDiaoLuo.name = jsonobject["name"].Str;
					caiYaoDiaoLuo.FuBen = jsonobject["FuBen"].Str;
					if (CaiYaoDiaoLuo.DataDict.ContainsKey(caiYaoDiaoLuo.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", caiYaoDiaoLuo.id));
					}
					else
					{
						CaiYaoDiaoLuo.DataDict.Add(caiYaoDiaoLuo.id, caiYaoDiaoLuo);
						CaiYaoDiaoLuo.DataList.Add(caiYaoDiaoLuo);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CaiYaoDiaoLuo.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CaiYaoDiaoLuo.OnInitFinishAction != null)
			{
				CaiYaoDiaoLuo.OnInitFinishAction();
			}
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003916 RID: 14614
		public static Dictionary<int, CaiYaoDiaoLuo> DataDict = new Dictionary<int, CaiYaoDiaoLuo>();

		// Token: 0x04003917 RID: 14615
		public static List<CaiYaoDiaoLuo> DataList = new List<CaiYaoDiaoLuo>();

		// Token: 0x04003918 RID: 14616
		public static Action OnInitFinishAction = new Action(CaiYaoDiaoLuo.OnInitFinish);

		// Token: 0x04003919 RID: 14617
		public int id;

		// Token: 0x0400391A RID: 14618
		public int type;

		// Token: 0x0400391B RID: 14619
		public int MapIndex;

		// Token: 0x0400391C RID: 14620
		public int ThreeSence;

		// Token: 0x0400391D RID: 14621
		public int value1;

		// Token: 0x0400391E RID: 14622
		public int value2;

		// Token: 0x0400391F RID: 14623
		public int value3;

		// Token: 0x04003920 RID: 14624
		public int value4;

		// Token: 0x04003921 RID: 14625
		public int value5;

		// Token: 0x04003922 RID: 14626
		public int value6;

		// Token: 0x04003923 RID: 14627
		public int value7;

		// Token: 0x04003924 RID: 14628
		public int value8;

		// Token: 0x04003925 RID: 14629
		public string name;

		// Token: 0x04003926 RID: 14630
		public string FuBen;
	}
}
