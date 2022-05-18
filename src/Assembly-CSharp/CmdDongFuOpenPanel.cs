using System;
using Fungus;
using UnityEngine;

// Token: 0x02000311 RID: 785
[CommandInfo("YSDongFu", "打开面板", "打开面板", 0)]
[AddComponentMenu("")]
public class CmdDongFuOpenPanel : Command
{
	// Token: 0x06001752 RID: 5970 RVA: 0x000CD6DC File Offset: 0x000CB8DC
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

	// Token: 0x06001753 RID: 5971 RVA: 0x00014948 File Offset: 0x00012B48
	public void OpenLianQi()
	{
		if (this.DongFu.AreaUnlock[0] == 0)
		{
			DongFuScene.Inst.OnLianQiBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLianQiBuildFuncClick();
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x00014972 File Offset: 0x00012B72
	public void OpenLianDan()
	{
		if (this.DongFu.AreaUnlock[1] == 0)
		{
			DongFuScene.Inst.OnLianDanBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLianDanBuildFuncClick();
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x0001499C File Offset: 0x00012B9C
	public void OpenLingTian()
	{
		if (this.DongFu.AreaUnlock[2] == 0)
		{
			DongFuScene.Inst.OnLingTianBreakFuncClick();
			return;
		}
		DongFuScene.Inst.OnLingTianBuildFuncClick();
	}

	// Token: 0x040012B5 RID: 4789
	[SerializeField]
	protected DongFuArea Panel;

	// Token: 0x040012B6 RID: 4790
	private DongFuData DongFu;
}
