using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000825 RID: 2085
	public class CyRandomTaskFailData : IJSONClass
	{
		// Token: 0x06003EA6 RID: 16038 RVA: 0x001AC698 File Offset: 0x001AA898
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyRandomTaskFailData.list)
			{
				try
				{
					CyRandomTaskFailData cyRandomTaskFailData = new CyRandomTaskFailData();
					cyRandomTaskFailData.id = jsonobject["id"].I;
					cyRandomTaskFailData.ShiBaiInfo2 = jsonobject["ShiBaiInfo2"].I;
					cyRandomTaskFailData.ShiBaiInfo3 = jsonobject["ShiBaiInfo3"].I;
					cyRandomTaskFailData.ShiBaiInfo4 = jsonobject["ShiBaiInfo4"].I;
					if (CyRandomTaskFailData.DataDict.ContainsKey(cyRandomTaskFailData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyRandomTaskFailData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyRandomTaskFailData.id));
					}
					else
					{
						CyRandomTaskFailData.DataDict.Add(cyRandomTaskFailData.id, cyRandomTaskFailData);
						CyRandomTaskFailData.DataList.Add(cyRandomTaskFailData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyRandomTaskFailData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyRandomTaskFailData.OnInitFinishAction != null)
			{
				CyRandomTaskFailData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A44 RID: 14916
		public static Dictionary<int, CyRandomTaskFailData> DataDict = new Dictionary<int, CyRandomTaskFailData>();

		// Token: 0x04003A45 RID: 14917
		public static List<CyRandomTaskFailData> DataList = new List<CyRandomTaskFailData>();

		// Token: 0x04003A46 RID: 14918
		public static Action OnInitFinishAction = new Action(CyRandomTaskFailData.OnInitFinish);

		// Token: 0x04003A47 RID: 14919
		public int id;

		// Token: 0x04003A48 RID: 14920
		public int ShiBaiInfo2;

		// Token: 0x04003A49 RID: 14921
		public int ShiBaiInfo3;

		// Token: 0x04003A4A RID: 14922
		public int ShiBaiInfo4;
	}
}
