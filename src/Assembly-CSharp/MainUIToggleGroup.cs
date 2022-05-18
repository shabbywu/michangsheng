using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public class MainUIToggleGroup : MonoBehaviour
{
	// Token: 0x06001F08 RID: 7944 RVA: 0x0010AC14 File Offset: 0x00108E14
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

	// Token: 0x04001A7C RID: 6780
	[HideInInspector]
	public List<MainUIToggle> toggleList;

	// Token: 0x04001A7D RID: 6781
	[HideInInspector]
	public MainUIToggle curToggle;
}
