using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082F RID: 2095
	public class DiYuShengWangData : IJSONClass
	{
		// Token: 0x06003ECE RID: 16078 RVA: 0x001AD524 File Offset: 0x001AB724
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

		// Token: 0x06003ECF RID: 16079 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A85 RID: 14981
		public static Dictionary<int, DiYuShengWangData> DataDict = new Dictionary<int, DiYuShengWangData>();

		// Token: 0x04003A86 RID: 14982
		public static List<DiYuShengWangData> DataList = new List<DiYuShengWangData>();

		// Token: 0x04003A87 RID: 14983
		public static Action OnInitFinishAction = new Action(DiYuShengWangData.OnInitFinish);

		// Token: 0x04003A88 RID: 14984
		public int id;

		// Token: 0x04003A89 RID: 14985
		public int ShiLi;

		// Token: 0x04003A8A RID: 14986
		public int ShengWangLV;

		// Token: 0x04003A8B RID: 14987
		public int ShenFen;

		// Token: 0x04003A8C RID: 14988
		public string TeQuan;
	}
}
