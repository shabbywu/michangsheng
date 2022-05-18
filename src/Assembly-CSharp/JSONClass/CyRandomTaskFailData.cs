using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBB RID: 3003
	public class CyRandomTaskFailData : IJSONClass
	{
		// Token: 0x06004A54 RID: 19028 RVA: 0x001F7954 File Offset: 0x001F5B54
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

		// Token: 0x06004A55 RID: 19029 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045CC RID: 17868
		public static Dictionary<int, CyRandomTaskFailData> DataDict = new Dictionary<int, CyRandomTaskFailData>();

		// Token: 0x040045CD RID: 17869
		public static List<CyRandomTaskFailData> DataList = new List<CyRandomTaskFailData>();

		// Token: 0x040045CE RID: 17870
		public static Action OnInitFinishAction = new Action(CyRandomTaskFailData.OnInitFinish);

		// Token: 0x040045CF RID: 17871
		public int id;

		// Token: 0x040045D0 RID: 17872
		public int ShiBaiInfo2;

		// Token: 0x040045D1 RID: 17873
		public int ShiBaiInfo3;

		// Token: 0x040045D2 RID: 17874
		public int ShiBaiInfo4;
	}
}
