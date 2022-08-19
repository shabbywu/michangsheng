using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FEE RID: 4078
	[TaskDescription("The random probability task will return success when the random probability is above the succeed probability. It will otherwise return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=33")]
	public class RandomProbability : Conditional
	{
		// Token: 0x060070EA RID: 28906 RVA: 0x002AACF1 File Offset: 0x002A8EF1
		public override void OnAwake()
		{
			if (this.useSeed.Value)
			{
				this.random = new Random(this.seed.Value);
				return;
			}
			this.random = new Random();
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x002AAD22 File Offset: 0x002A8F22
		public override TaskStatus OnUpdate()
		{
			if ((float)this.random.NextDouble() < this.successProbability.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x002AAD40 File Offset: 0x002A8F40
		public override void OnReset()
		{
			this.successProbability = 0.5f;
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x04005CE9 RID: 23785
		[Tooltip("The chance that the task will return success")]
		public SharedFloat successProbability = 0.5f;

		// Token: 0x04005CEA RID: 23786
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public SharedInt seed;

		// Token: 0x04005CEB RID: 23787
		[Tooltip("Do we want to use the seed?")]
		public SharedBool useSeed;

		// Token: 0x04005CEC RID: 23788
		private Random random;
	}
}
