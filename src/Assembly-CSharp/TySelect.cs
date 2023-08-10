using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TySelect : MonoBehaviour
{
	public Text desc;

	public FpBtn okBtn;

	public FpBtn returnBtn;

	private static TySelect _inst;

	public static TySelect inst
	{
		get
		{
			if ((Object)(object)_inst == (Object)null)
			{
				_inst = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("TySelect")).GetComponent<TySelect>();
			}
			return _inst;
		}
	}

	public void Show(string mag, UnityAction ok = null, UnityAction quit = null, bool isDestorySelf = true)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		desc.text = mag;
		if (ok != null)
		{
			okBtn.mouseUpEvent.AddListener(ok);
		}
		if (isDestorySelf)
		{
			okBtn.mouseUpEvent.AddListener(new UnityAction(Close));
		}
		if (quit != null)
		{
			returnBtn.mouseUpEvent.AddListener(quit);
		}
		returnBtn.mouseUpEvent.AddListener(new UnityAction(Close));
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
