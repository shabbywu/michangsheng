using System;

// Token: 0x0200001F RID: 31
public class LTEvent
{
	// Token: 0x06000169 RID: 361 RVA: 0x000088B8 File Offset: 0x00006AB8
	public LTEvent(int id, object data)
	{
		this.id = id;
		this.data = data;
	}

	// Token: 0x04000114 RID: 276
	public int id;

	// Token: 0x04000115 RID: 277
	public object data;
}
