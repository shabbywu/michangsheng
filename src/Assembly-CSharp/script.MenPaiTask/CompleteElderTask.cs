using UnityEngine.Events;
using UnityEngine.UI;

namespace script.MenPaiTask;

public class CompleteElderTask : BaseElderTask
{
	protected override void Init()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		base.Init();
		Get<FpBtn>("领取按钮/按钮").mouseUpEvent.AddListener((UnityAction)delegate
		{
			Tools.instance.getPlayer().ElderTaskMag.PlayerGetTaskItem(ElderTask);
			DestroySelf();
		});
		if (NPCEx.IsDeath(ElderTask.NpcId))
		{
			Get("头像").SetActive(false);
			return;
		}
		Get<PlayerSetRandomFace>("头像/Head/Viewport/Content/FaceBase/SkeletonGraphic").SetNPCFace(ElderTask.NpcId);
		Get<Text>("头像/Title/TitleText").SetText(jsonData.instance.AvatarJsonData[ElderTask.NpcId.ToString()]["Title"].Str);
		Get<Text>("头像/Name").SetText(jsonData.instance.AvatarRandomJsonData[ElderTask.NpcId.ToString()]["Name"].Str);
	}
}
