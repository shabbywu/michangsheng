using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QiYu;

public class QiYuOption : MonoBehaviour
{
	[SerializeField]
	private FpBtn Btn;

	[SerializeField]
	private Text OptionName;

	public void Init(string Name, UnityAction Action)
	{
		OptionName.text = Name;
		Btn.mouseUpEvent.AddListener(Action);
		((Component)this).gameObject.SetActive(true);
	}
}
