using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bag;

public class BaseFilterTopChild : FpBtn
{
	public Text Text;

	public void Init(string title, UnityAction action)
	{
		Text.SetText(title);
		mouseUpEvent.AddListener(action);
		((Component)this).gameObject.SetActive(true);
	}
}
