using System;

// Token: 0x020003BD RID: 957
public class ConsumeLimitCD
{
	// Token: 0x06001F3F RID: 7999 RVA: 0x000027FC File Offset: 0x000009FC
	private ConsumeLimitCD()
	{
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000DBF0F File Offset: 0x000DA10F
	public void Update(float deltaTime)
	{
		if (this.restTime > 0f)
		{
			this.restTime -= deltaTime;
		}
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x000DBF2C File Offset: 0x000DA12C
	public bool isWaiting()
	{
		return this.restTime > 0f;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x000DBF3B File Offset: 0x000DA13B
	public void Start(float time)
	{
		this.totalTime = time;
		this.restTime = this.totalTime;
	}

	// Token: 0x0400195F RID: 6495
	public static readonly ConsumeLimitCD instance = new ConsumeLimitCD();

	// Token: 0x04001960 RID: 6496
	public float restTime;

	// Token: 0x04001961 RID: 6497
	public float totalTime;
}
