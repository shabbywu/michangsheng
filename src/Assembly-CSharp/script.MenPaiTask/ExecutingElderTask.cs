using UnityEngine.UI;

namespace script.MenPaiTask;

public class ExecutingElderTask : BaseElderTask
{
	protected override void Init()
	{
		if (NPCEx.IsDeath(ElderTask.NpcId))
		{
			_go.SetActive(false);
			return;
		}
		base.Init();
		Get<PlayerSetRandomFace>("头像/Head/Viewport/Content/FaceBase/SkeletonGraphic").SetNPCFace(ElderTask.NpcId);
		Get<Text>("头像/Title/TitleText").SetText(jsonData.instance.AvatarJsonData[ElderTask.NpcId.ToString()]["Title"].Str);
		Get<Text>("头像/Name").SetText(jsonData.instance.AvatarRandomJsonData[ElderTask.NpcId.ToString()]["Name"].Str);
	}
}
