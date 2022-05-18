using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014A6 RID: 5286
	[TaskDescription("The random probability task will return success when the random probability is above the succeed probability. It will otherwise return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=33")]
	public class RandomProbability : Conditional
	{
		// Token: 0x06007EE4 RID: 32484 RVA: 0x00055EFC File Offset: 0x000540FC
		public override void OnAwake()
		{
			if (this.useSeed.Value)
			{
				this.random = new Random(this.seed.Value);
				return;
			}
			this.random = new Random();
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x00055F2D File Offset: 0x0005412D
		public override TaskStatus OnUpdate()
		{
			if ((float)this.random.NextDouble() < this.successProbability.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x00055F4B File Offset: 0x0005414B
		public override void OnReset()
		{
			this.successProbability = 0.5f;
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x04006BE1 RID: 27617
		[Tooltip("The chance that the task will return success")]
		public SharedFloat successProbability = 0.5f;

		// Token: 0x04006BE2 RID: 27618
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public SharedInt seed;

		// Token: 0x04006BE3 RID: 27619
		[Tooltip("Do we want to use the seed?")]
		public SharedBool useSeed;

		// Token: 0x04006BE4 RID: 27620
		private Random random;
	}
}
