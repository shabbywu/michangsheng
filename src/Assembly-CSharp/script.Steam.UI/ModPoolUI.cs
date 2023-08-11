using UnityEngine;
using script.NewLianDan.Base;
using script.Steam.Ctr;

namespace script.Steam.UI;

public class ModPoolUI : BasePanel
{
	private bool isInit;

	public GameObject ModPrefab;

	public ModUIPoolCtr Ctr;

	public Transform Load;

	public ModPoolUI(GameObject gameObject)
	{
		_go = gameObject;
		Ctr = new ModUIPoolCtr();
		ModPrefab = Get("Scroll View/Viewport/Content/Mod");
	}

	public override void Show()
	{
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		base.Show();
	}

	private void Init()
	{
	}
}
