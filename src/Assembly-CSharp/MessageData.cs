using System;

// Token: 0x020001ED RID: 493
public class MessageData
{
	// Token: 0x06001467 RID: 5223 RVA: 0x000833C7 File Offset: 0x000815C7
	public MessageData(bool value)
	{
		this.valueBool = value;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x000833D6 File Offset: 0x000815D6
	public MessageData(int value)
	{
		this.valueInt = value;
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x000833E5 File Offset: 0x000815E5
	public MessageData(float value)
	{
		this.valueFloat = value;
	}

	// Token: 0x04000F25 RID: 3877
	public bool valueBool;

	// Token: 0x04000F26 RID: 3878
	public int valueInt;

	// Token: 0x04000F27 RID: 3879
	public float valueFloat;
}
