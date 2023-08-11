using Fungus;
using UnityEngine;

[CommandInfo("YSDongFu", "打开面板", "打开面板", 0)]
[AddComponentMenu("")]
public class CmdDongFuOpenPanel : Command
{
	[SerializeField]
	protected DongFuArea Panel;

	private DongFuData DongFu;

	public override void OnEnter()
	{
		DongFu = new DongFuData(DongFuManager.NowDongFuID);
		DongFu.Load();
		if (Panel == DongFuArea.炼器)
		{
			OpenLianQi();
		}
		else if (Panel == DongFuArea.炼丹)
		{
			OpenLianDan();
		}
		else if (Panel == DongFuArea.灵田)
		{
			OpenLingTian();
		}
		Continue();
	}

	public void OpenLianQi()
	{
		if (DongFu.AreaUnlock[0] == 0)
		{
			DongFuScene.Inst.OnLianQiBreakFuncClick();
		}
		else
		{
			DongFuScene.Inst.OnLianQiBuildFuncClick();
		}
	}

	public void OpenLianDan()
	{
		if (DongFu.AreaUnlock[1] == 0)
		{
			DongFuScene.Inst.OnLianDanBreakFuncClick();
		}
		else
		{
			DongFuScene.Inst.OnLianDanBuildFuncClick();
		}
	}

	public void OpenLingTian()
	{
		if (DongFu.AreaUnlock[2] == 0)
		{
			DongFuScene.Inst.OnLingTianBreakFuncClick();
		}
		else
		{
			DongFuScene.Inst.OnLingTianBuildFuncClick();
		}
	}
}
