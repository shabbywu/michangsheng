using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000742 RID: 1858
	public class _FaBaoLastNameJsonData : IJSONClass
	{
		// Token: 0x06003B1C RID: 15132 RVA: 0x0019683C File Offset: 0x00194A3C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._FaBaoLastNameJsonData.list)
			{
				try
				{
					_FaBaoLastNameJsonData faBaoLastNameJsonData = new _FaBaoLastNameJsonData();
					faBaoLastNameJsonData.id = jsonobject["id"].I;
					faBaoLastNameJsonData.PosReverse = jsonobject["PosReverse"].I;
					faBaoLastNameJsonData.LastName = jsonobject["LastName"].Str;
					faBaoLastNameJsonData.Type = jsonobject["Type"].ToList();
					faBaoLastNameJsonData.quality = jsonobject["quality"].ToList();
					if (_FaBaoLastNameJsonData.DataDict.ContainsKey(faBaoLastNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", faBaoLastNameJsonData.id));
					}
					else
					{
						_FaBaoLastNameJsonData.DataDict.Add(faBaoLastNameJsonData.id, faBaoLastNameJsonData);
						_FaBaoLastNameJsonData.DataList.Add(faBaoLastNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_FaBaoLastNameJsonData.OnInitFinishAction != null)
			{
				_FaBaoLastNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003362 RID: 13154
		public static Dictionary<int, _FaBaoLastNameJsonData> DataDict = new Dictionary<int, _FaBaoLastNameJsonData>();

		// Token: 0x04003363 RID: 13155
		public static List<_FaBaoLastNameJsonData> DataList = new List<_FaBaoLastNameJsonData>();

		// Token: 0x04003364 RID: 13156
		public static Action OnInitFinishAction = new Action(_FaBaoLastNameJsonData.OnInitFinish);

		// Token: 0x04003365 RID: 13157
		public int id;

		// Token: 0x04003366 RID: 13158
		public int PosReverse;

		// Token: 0x04003367 RID: 13159
		public string LastName;

		// Token: 0x04003368 RID: 13160
		public List<int> Type = new List<int>();

		// Token: 0x04003369 RID: 13161
		public List<int> quality = new List<int>();
	}
}
