using UltimateSurvival;
using UnityEngine;

public class UI_Backgroud : UltimateSurvival.MonoSingleton<UI_Backgroud>
{
	private bool _value;

	public bool Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
			((Component)this).gameObject.SetActive(value);
		}
	}

	private void Start()
	{
		((Component)UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance).gameObject.SetActive(false);
	}

	public void OpenLayer(GameObject obj, bool _v)
	{
		Value = _v;
		playOpen(obj);
	}

	public void playOpen(GameObject obj)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		obj.transform.localScale = Vector3.zero;
		iTween.ScaleTo(obj, iTween.Hash(new object[10] { "x", 1, "y", 1, "z", 1, "time", 1.0, "islocal", true }));
	}
}
