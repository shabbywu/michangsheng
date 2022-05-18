using System;

// Token: 0x02000216 RID: 534
[Serializable]
public class ItemAttribute
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x00010735 File Offset: 0x0000E935
	public ItemAttribute(string attributeName, int attributeValue)
	{
		this.attributeName = attributeName;
		this.attributeValue = attributeValue;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0000403D File Offset: 0x0000223D
	public ItemAttribute()
	{
	}

	// Token: 0x04000D56 RID: 3414
	public string attributeName;

	// Token: 0x04000D57 RID: 3415
	public int attributeValue;
}
