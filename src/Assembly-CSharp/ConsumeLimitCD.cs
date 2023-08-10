public class ConsumeLimitCD
{
	public static readonly ConsumeLimitCD instance = new ConsumeLimitCD();

	public float restTime;

	public float totalTime;

	private ConsumeLimitCD()
	{
	}

	public void Update(float deltaTime)
	{
		if (restTime > 0f)
		{
			restTime -= deltaTime;
		}
	}

	public bool isWaiting()
	{
		return restTime > 0f;
	}

	public void Start(float time)
	{
		totalTime = time;
		restTime = totalTime;
	}
}
