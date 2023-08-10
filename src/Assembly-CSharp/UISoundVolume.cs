using UnityEngine;

[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	private UISlider mSlider;

	private void Awake()
	{
		mSlider = ((Component)this).GetComponent<UISlider>();
		mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(mSlider.onChange, OnChange);
	}

	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}
}
