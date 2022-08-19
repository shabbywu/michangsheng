using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC9 RID: 3785
	[Serializable]
	public class SaveDataItem
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06006AE1 RID: 27361 RVA: 0x0029475F File Offset: 0x0029295F
		public virtual string DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x00294767 File Offset: 0x00292967
		public virtual string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0029476F File Offset: 0x0029296F
		public static SaveDataItem Create(string dataType, string data)
		{
			return new SaveDataItem
			{
				dataType = dataType,
				data = data
			};
		}

		// Token: 0x04005A30 RID: 23088
		[SerializeField]
		protected string dataType = "";

		// Token: 0x04005A31 RID: 23089
		[SerializeField]
		protected string data = "";
	}
}
