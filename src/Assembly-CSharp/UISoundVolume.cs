using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x06000652 RID: 1618 RVA: 0x000099E8 File Offset: 0x00007BE8
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00009A23 File Offset: 0x00007C23
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}

	// Token: 0x040004B5 RID: 1205
	private UISlider mSlider;
}
