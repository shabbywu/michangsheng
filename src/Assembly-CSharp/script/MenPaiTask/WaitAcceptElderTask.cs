using System;

namespace script.MenPaiTask
{
	// Token: 0x02000A0C RID: 2572
	public class WaitAcceptElderTask : BaseElderTask
	{
		// Token: 0x0600473C RID: 18236 RVA: 0x001E2B29 File Offset: 0x001E0D29
		protected override void Init()
		{
			base.Init();
			base.Get<FpBtn>("取消任务").mouseUpEvent.AddListener(delegate()
			{
				Tools.instance.getPlayer().ElderTaskMag.PlayerCancelTask(this.ElderTask);
				this.DestroySelf();
			});
		}
	}
}
