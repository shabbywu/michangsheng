using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000741 RID: 1857
	public class _FaBaoFirstNameJsonData : IJSONClass
	{
		// Token: 0x06003B18 RID: 15128 RVA: 0x00196674 File Offset: 0x00194874
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._FaBaoFirstNameJsonData.list)
			{
				try
				{
					_FaBaoFirstNameJsonData faBaoFirstNameJsonData = new _FaBaoFirstNameJsonData();
					faBaoFirstNameJsonData.id = jsonobject["id"].I;
					faBaoFirstNameJsonData.PosReverse = jsonobject["PosReverse"].I;
					faBaoFirstNameJsonData.FirstName = jsonobject["FirstName"].Str;
					faBaoFirstNameJsonData.Type = jsonobject["Type"].ToList();
					faBaoFirstNameJsonData.quality = jsonobject["quality"].ToList();
					if (_FaBaoFirstNameJsonData.DataDict.ContainsKey(faBaoFirstNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", faBaoFirstNameJsonData.id));
					}
					else
					{
						_FaBaoFirstNameJsonData.DataDict.Add(faBaoFirstNameJsonData.id, faBaoFirstNameJsonData);
						_FaBaoFirstNameJsonData.DataList.Add(faBaoFirstNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_FaBaoFirstNameJsonData.OnInitFinishAction != null)
			{
				_FaBaoFirstNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400335A RID: 13146
		public static Dictionary<int, _FaBaoFirstNameJsonData> DataDict = new Dictionary<int, _FaBaoFirstNameJsonData>();

		// Token: 0x0400335B RID: 13147
		public static List<_FaBaoFirstNameJsonData> DataList = new List<_FaBaoFirstNameJsonData>();

		// Token: 0x0400335C RID: 13148
		public static Action OnInitFinishAction = new Action(_FaBaoFirstNameJsonData.OnInitFinish);

		// Token: 0x0400335D RID: 13149
		public int id;

		// Token: 0x0400335E RID: 13150
		public int PosReverse;

		// Token: 0x0400335F RID: 13151
		public string FirstName;

		// Token: 0x04003360 RID: 13152
		public List<int> Type = new List<int>();

		// Token: 0x04003361 RID: 13153
		public List<int> quality = new List<int>();
	}
}
