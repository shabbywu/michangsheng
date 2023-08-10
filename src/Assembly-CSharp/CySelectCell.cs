using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CySelectCell : MonoBehaviour
{
	public FpBtn btn;

	public Text selectName;

	public void Init(string name, UnityAction action)
	{
		btn.mouseUpEvent.AddListener(action);
		selectName.text = name;
		((Component)this).gameObject.SetActive(true);
	}
}
