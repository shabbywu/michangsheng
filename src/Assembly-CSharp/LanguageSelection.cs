using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x060004D0 RID: 1232 RVA: 0x00070118 File Offset: 0x0006E318
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		if (Localization.knownLanguages != null)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000083A9 File Offset: 0x000065A9
	private void OnChange()
	{
		Localization.language = UIPopupList.current.value;
	}

	// Token: 0x04000328 RID: 808
	private UIPopupList mList;
}
