using System;

// Token: 0x02000023 RID: 35
public class LTEvent
{
	// Token: 0x0600016F RID: 367 RVA: 0x0000521E File Offset: 0x0000341E
	public LTEvent(int id, object data)
	{
		this.id = id;
		this.data = data;
	}

	// Token: 0x04000123 RID: 291
	public int id;

	// Token: 0x04000124 RID: 292
	public object data;
}
