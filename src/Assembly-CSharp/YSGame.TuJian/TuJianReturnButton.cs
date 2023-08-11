using UnityEngine;
using WXB;

namespace YSGame.TuJian;

public class TuJianReturnButton : MonoBehaviour
{
	private SymbolText _HyText;

	private bool isShow;

	private void Start()
	{
		_HyText = ((Component)this).gameObject.GetComponent<SymbolText>();
		isShow = ((Behaviour)_HyText).enabled;
	}

	private void Update()
	{
		if (TuJianManager.Inst.CanReturn())
		{
			if (!isShow)
			{
				((Behaviour)_HyText).enabled = true;
				isShow = true;
			}
		}
		else if (isShow)
		{
			((Behaviour)_HyText).enabled = false;
			isShow = false;
		}
	}
}
