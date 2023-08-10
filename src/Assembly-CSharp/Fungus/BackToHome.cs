using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus;

[CommandInfo("YSTools", "BackToHome", "直接提升一个等级，并把经验值设为0", 0)]
[AddComponentMenu("")]
public class BackToHome : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "返回到主界面，不用填什么";

	public void setHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	public override void OnEnter()
	{
		YSGame.YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		SceneManager.LoadScene("MainMenu");
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
