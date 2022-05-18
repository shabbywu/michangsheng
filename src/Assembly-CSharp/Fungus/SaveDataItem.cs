using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001367 RID: 4967
	[Serializable]
	public class SaveDataItem
	{
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600787C RID: 30844 RVA: 0x00051D72 File Offset: 0x0004FF72
		public virtual string DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x00051D7A File Offset: 0x0004FF7A
		public virtual string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x00051D82 File Offset: 0x0004FF82
		public static SaveDataItem Create(string dataType, string data)
		{
			return new SaveDataItem
			{
				dataType = dataType,
				data = data
			};
		}

		// Token: 0x0400688F RID: 26767
		[SerializeField]
		protected string dataType = "";

		// Token: 0x04006890 RID: 26768
		[SerializeField]
		protected string data = "";
	}
}
