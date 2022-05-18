using System;

// Token: 0x02000301 RID: 769
public class MessageData
{
	// Token: 0x06001711 RID: 5905 RVA: 0x000145F7 File Offset: 0x000127F7
	public MessageData(bool value)
	{
		this.valueBool = value;
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00014606 File Offset: 0x00012806
	public MessageData(int value)
	{
		this.valueInt = value;
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x00014615 File Offset: 0x00012815
	public MessageData(float value)
	{
		this.valueFloat = value;
	}

	// Token: 0x04001268 RID: 4712
	public bool valueBool;

	// Token: 0x04001269 RID: 4713
	public int valueInt;

	// Token: 0x0400126A RID: 4714
	public float valueFloat;
}
