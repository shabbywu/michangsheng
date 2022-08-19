using System;
using Fungus;
using UnityEngine;

// Token: 0x020001FC RID: 508
[CommandInfo("YSDongFu", "打开面板", "打开面板", 0)]
[AddComponentMenu("")]
public class CmdDongFuOpenPanel : Command
{
	// Token: 0x060014A8 RID: 5288 RVA: 0x00084B38 File Offset: 0x00082D38
	public override void OnEnter()
	{
		this.DongFu = new DongFuData(DongFuManager.NowDongFuID);
		this.DongFu.Load();
		if (this.Panel == DongFuArea.炼器)
		{
			this.OpenLianQi();
		}
		else if (this.Panel == DongFuArea.炼丹)
		{
			this.OpenLianDan();
		}
		else if (this.Panel == DongFuArea.灵田)
		{
			this.OpenLingTian();
		}
		this.Continue();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x00084B96 File Offset: 0x00082D96
	public void OpenLianQi()
	{
		if (this.DongFu.AreaUnlock[0] == 0)
		{
			DongFuScene.Inst.OnLianQiBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLianQiBuildFuncClick();
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x00084BC0 File Offset: 0x00082DC0
	public void OpenLianDan()
	{
		if (this.DongFu.AreaUnlock[1] == 0)
		{
			DongFuScene.Inst.OnLianDanBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLianDanBuildFuncClick();
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x00084BEA File Offset: 0x00082DEA
	public void OpenLingTian()
	{
		if (this.DongFu.AreaUnlock[2] == 0)
		{
			DongFuScene.Inst.OnLingTianBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLingTianBuildFuncClick();
	}

	// Token: 0x04000F6F RID: 3951
	[SerializeField]
	protected DongFuArea Panel;

	// Token: 0x04000F70 RID: 3952
	private DongFuData DongFu;
}
