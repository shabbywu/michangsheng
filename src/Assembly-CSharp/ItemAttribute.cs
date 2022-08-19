using System;

// Token: 0x02000141 RID: 321
[Serializable]
public class ItemAttribute
{
	// Token: 0x06000EA9 RID: 3753 RVA: 0x000598E2 File Offset: 0x00057AE2
	public ItemAttribute(string attributeName, int attributeValue)
	{
		this.attributeName = attributeName;
		this.attributeValue = attributeValue;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x000027FC File Offset: 0x000009FC
	public ItemAttribute()
	{
	}

	// Token: 0x04000ABB RID: 2747
	public string attributeName;

	// Token: 0x04000ABC RID: 2748
	public int attributeValue;
}
