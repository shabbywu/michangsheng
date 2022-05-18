using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001492 RID: 5266
	[TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=22")]
	[TaskIcon("{SkinColor}WaitIcon.png")]
	public class Wait : Action
	{
		// Token: 0x06007E4F RID: 32335 RVA: 0x002C8B08 File Offset: 0x002C6D08
		public override void OnStart()
		{
			this.startTime = Time.time;
			if (this.randomWait.Value)
			{
				this.waitDuration = Random.Range(this.randomWaitMin.Value, this.randomWaitMax.Value);
				return;
			}
			this.waitDuration = this.waitTime.Value;
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x0005570A File Offset: 0x0005390A
		public override TaskStatus OnUpdate()
		{
			if (this.startTime + this.waitDuration < Time.time)
			{
				return 2;
			}
			return 3;
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x00055723 File Offset: 0x00053923
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this.pauseTime = Time.time;
				return;
			}
			this.startTime += Time.time - this.pauseTime;
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x0005574D File Offset: 0x0005394D
		public override void OnReset()
		{
			this.waitTime = 1f;
			this.randomWait = false;
			this.randomWaitMin = 1f;
			this.randomWaitMax = 1f;
		}

		// Token: 0x04006B9C RID: 27548
		[Tooltip("The amount of time to wait")]
		public SharedFloat waitTime = 1f;

		// Token: 0x04006B9D RID: 27549
		[Tooltip("Should the wait be randomized?")]
		public SharedBool randomWait = false;

		// Token: 0x04006B9E RID: 27550
		[Tooltip("The minimum wait time if random wait is enabled")]
		public SharedFloat randomWaitMin = 1f;

		// Token: 0x04006B9F RID: 27551
		[Tooltip("The maximum wait time if random wait is enabled")]
		public SharedFloat randomWaitMax = 1f;

		// Token: 0x04006BA0 RID: 27552
		private float waitDuration;

		// Token: 0x04006BA1 RID: 27553
		private float startTime;

		// Token: 0x04006BA2 RID: 27554
		private float pauseTime;
	}
}
