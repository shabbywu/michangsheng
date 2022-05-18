using System;

// Token: 0x02000550 RID: 1360
public class ConsumeLimitCD
{
	// Token: 0x060022CB RID: 8907 RVA: 0x0000403D File Offset: 0x0000223D
	private ConsumeLimitCD()
	{
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x0001C6D5 File Offset: 0x0001A8D5
	public void Update(float deltaTime)
	{
		if (this.restTime > 0f)
		{
			this.restTime -= deltaTime;
		}
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x0001C6F2 File Offset: 0x0001A8F2
	public bool isWaiting()
	{
		return this.restTime > 0f;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x0001C701 File Offset: 0x0001A901
	public void Start(float time)
	{
		this.totalTime = time;
		this.restTime = this.totalTime;
	}

	// Token: 0x04001DE5 RID: 7653
	public static readonly ConsumeLimitCD instance = new ConsumeLimitCD();

	// Token: 0x04001DE6 RID: 7654
	public float restTime;

	// Token: 0x04001DE7 RID: 7655
	public float totalTime;
}
