using UnityEngine.Events;

namespace script.MenPaiTask;

public class WaitAcceptElderTask : BaseElderTask
{
	protected override void Init()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		base.Init();
		Get<FpBtn>("取消任务").mouseUpEvent.AddListener((UnityAction)delegate
		{
			Tools.instance.getPlayer().ElderTaskMag.PlayerCancelTask(ElderTask);
			DestroySelf();
		});
	}
}
