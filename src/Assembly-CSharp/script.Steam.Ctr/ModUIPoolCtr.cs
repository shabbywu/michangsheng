using System.Collections.Generic;
using UnityEngine;
using script.Steam.UI;
using script.Steam.UI.Base;

namespace script.Steam.Ctr;

public class ModUIPoolCtr
{
	private int CurNum;

	private int Max = 50;

	private readonly List<ModUI> ModUIPools;

	public ModPoolUI UI => WorkShopMag.Inst.ModPoolUI;

	public ModUIPoolCtr()
	{
		ModUIPools = new List<ModUI>(Max);
	}

	public ModUI GetModUI(int type = 0)
	{
		foreach (ModUI modUIPool in ModUIPools)
		{
			if (!modUIPool.IsUsed)
			{
				modUIPool.SetType(type);
				return modUIPool;
			}
		}
		if (CurNum == Max)
		{
			Debug.LogError((object)"超出最大限制");
			return null;
		}
		CurNum++;
		ModUI modUI = new ModUI(UI.ModPrefab.Inst(UI.ModPrefab.transform.parent));
		modUI.SetType(type);
		ModUIPools.Add(modUI);
		return modUI;
	}

	public void BackMod(ModUI modUI)
	{
		modUI.UnBindingInfo();
	}
}
