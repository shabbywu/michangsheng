using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC4 RID: 3012
	public class DiYuShengWangData : IJSONClass
	{
		// Token: 0x06004A78 RID: 19064 RVA: 0x001F852C File Offset: 0x001F672C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DiYuShengWangData.list)
			{
				try
				{
					DiYuShengWangData diYuShengWangData = new DiYuShengWangData();
					diYuShengWangData.id = jsonobject["id"].I;
					diYuShengWangData.ShiLi = jsonobject["ShiLi"].I;
					diYuShengWangData.ShengWangLV = jsonobject["ShengWangLV"].I;
					diYuShengWangData.ShenFen = jsonobject["ShenFen"].I;
					diYuShengWangData.TeQuan = jsonobject["TeQuan"].Str;
					if (DiYuShengWangData.DataDict.ContainsKey(diYuShengWangData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DiYuShengWangData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", diYuShengWangData.id));
					}
					else
					{
						DiYuShengWangData.DataDict.Add(diYuShengWangData.id, diYuShengWangData);
						DiYuShengWangData.DataList.Add(diYuShengWangData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DiYuShengWangData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DiYuShengWangData.OnInitFinishAction != null)
			{
				DiYuShengWangData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004609 RID: 17929
		public static Dictionary<int, DiYuShengWangData> DataDict = new Dictionary<int, DiYuShengWangData>();

		// Token: 0x0400460A RID: 17930
		public static List<DiYuShengWangData> DataList = new List<DiYuShengWangData>();

		// Token: 0x0400460B RID: 17931
		public static Action OnInitFinishAction = new Action(DiYuShengWangData.OnInitFinish);

		// Token: 0x0400460C RID: 17932
		public int id;

		// Token: 0x0400460D RID: 17933
		public int ShiLi;

		// Token: 0x0400460E RID: 17934
		public int ShengWangLV;

		// Token: 0x0400460F RID: 17935
		public int ShenFen;

		// Token: 0x04004610 RID: 17936
		public string TeQuan;
	}
}
