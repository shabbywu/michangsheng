using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FDA RID: 4058
	[TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=22")]
	[TaskIcon("{SkinColor}WaitIcon.png")]
	public class Wait : Action
	{
		// Token: 0x06007055 RID: 28757 RVA: 0x002A9874 File Offset: 0x002A7A74
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

		// Token: 0x06007056 RID: 28758 RVA: 0x002A98CC File Offset: 0x002A7ACC
		public override TaskStatus OnUpdate()
		{
			if (this.startTime + this.waitDuration < Time.time)
			{
				return 2;
			}
			return 3;
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x002A98E5 File Offset: 0x002A7AE5
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this.pauseTime = Time.time;
				return;
			}
			this.startTime += Time.time - this.pauseTime;
		}

		// Token: 0x06007058 RID: 28760 RVA: 0x002A990F File Offset: 0x002A7B0F
		public override void OnReset()
		{
			this.waitTime = 1f;
			this.randomWait = false;
			this.randomWaitMin = 1f;
			this.randomWaitMax = 1f;
		}

		// Token: 0x04005CA4 RID: 23716
		[Tooltip("The amount of time to wait")]
		public SharedFloat waitTime = 1f;

		// Token: 0x04005CA5 RID: 23717
		[Tooltip("Should the wait be randomized?")]
		public SharedBool randomWait = false;

		// Token: 0x04005CA6 RID: 23718
		[Tooltip("The minimum wait time if random wait is enabled")]
		public SharedFloat randomWaitMin = 1f;

		// Token: 0x04005CA7 RID: 23719
		[Tooltip("The maximum wait time if random wait is enabled")]
		public SharedFloat randomWaitMax = 1f;

		// Token: 0x04005CA8 RID: 23720
		private float waitDuration;

		// Token: 0x04005CA9 RID: 23721
		private float startTime;

		// Token: 0x04005CAA RID: 23722
		private float pauseTime;
	}
}
