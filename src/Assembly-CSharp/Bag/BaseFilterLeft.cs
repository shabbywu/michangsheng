using UnityEngine;
using UnityEngine.Events;

namespace Bag;

public class BaseFilterLeft : MonoBehaviour
{
	public ItemType ItemType;

	public FpBtn Select;

	public FpBtn UnSelect;

	public bool IsSelect;

	public void Add(UnityAction action)
	{
		Select.mouseUpEvent.AddListener(action);
		UnSelect.mouseUpEvent.AddListener(action);
	}

	public void UpdateState()
	{
		((Component)Select).gameObject.SetActive(IsSelect);
		((Component)UnSelect).gameObject.SetActive(!IsSelect);
	}
}
