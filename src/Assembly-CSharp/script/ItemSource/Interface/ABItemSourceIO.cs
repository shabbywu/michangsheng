using System;
using UnityEngine;

namespace script.ItemSource.Interface
{
	// Token: 0x02000A1D RID: 2589
	public abstract class ABItemSourceIO
	{
		// Token: 0x06004781 RID: 18305 RVA: 0x001E3EA6 File Offset: 0x001E20A6
		public virtual bool NeedCheckCount(int id)
		{
			return ABItemSource.Get().ItemSourceDataDic.ContainsKey(id);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001E3EB8 File Offset: 0x001E20B8
		public virtual bool Get(int id)
		{
			if (!ABItemSource.Get().ItemSourceDataDic.ContainsKey(id))
			{
				Debug.LogError("物品Id：" + id + "不在自动生成表中");
				return false;
			}
			if (ABItemSource.Get().ItemSourceDataDic[id].Count == 1)
			{
				ABItemSource.Get().ItemSourceDataDic[id].Count = 0;
				return true;
			}
			return false;
		}
	}
}
