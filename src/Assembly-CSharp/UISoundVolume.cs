using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x060005EA RID: 1514 RVA: 0x0002160F File Offset: 0x0001F80F
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0002164A File Offset: 0x0001F84A
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}

	// Token: 0x040003EE RID: 1006
	private UISlider mSlider;
}
