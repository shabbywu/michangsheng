using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class MainUIToggleGroup : MonoBehaviour
{
	// Token: 0x06001BC8 RID: 7112 RVA: 0x000C5B2C File Offset: 0x000C3D2C
	public void OnChildToggleChange(MainUIToggle toggle)
	{
		foreach (MainUIToggle mainUIToggle in this.toggleList)
		{
			if (!(mainUIToggle == toggle) && mainUIToggle.group == toggle.group && !mainUIToggle.isDisable)
			{
				mainUIToggle.isOn = !toggle.isOn;
				mainUIToggle.OnValueChange();
			}
		}
	}

	// Token: 0x04001657 RID: 5719
	[HideInInspector]
	public List<MainUIToggle> toggleList;

	// Token: 0x04001658 RID: 5720
	[HideInInspector]
	public MainUIToggle curToggle;
}
