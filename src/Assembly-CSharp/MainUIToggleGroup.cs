using System.Collections.Generic;
using UnityEngine;

public class MainUIToggleGroup : MonoBehaviour
{
	[HideInInspector]
	public List<MainUIToggle> toggleList;

	[HideInInspector]
	public MainUIToggle curToggle;

	public void OnChildToggleChange(MainUIToggle toggle)
	{
		foreach (MainUIToggle toggle2 in toggleList)
		{
			if (!((Object)(object)toggle2 == (Object)(object)toggle) && toggle2.group == toggle.group && !toggle2.isDisable)
			{
				toggle2.isOn = !toggle.isOn;
				toggle2.OnValueChange();
			}
		}
	}
}
