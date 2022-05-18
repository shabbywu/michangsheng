using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	// Token: 0x170001A7 RID: 423
	// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0008DF5C File Offset: 0x0008C15C
	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = base.GetComponent<UIWidget>();
				UILabel uilabel = component as UILabel;
				UISprite uisprite = component as UISprite;
				if (uilabel != null)
				{
					UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
					if (uiinput != null && uiinput.label == uilabel)
					{
						uiinput.defaultText = value;
						return;
					}
					uilabel.text = value;
					return;
				}
				else if (uisprite != null)
				{
					uisprite.spriteName = value;
					uisprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0000CF62 File Offset: 0x0000B162
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnLocalize();
		}
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0000CF72 File Offset: 0x0000B172
	private void Start()
	{
		this.mStarted = true;
		this.OnLocalize();
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0008DFD8 File Offset: 0x0008C1D8
	private void OnLocalize()
	{
		if (string.IsNullOrEmpty(this.key))
		{
			UILabel component = base.GetComponent<UILabel>();
			if (component != null)
			{
				this.key = component.text;
			}
		}
		if (!string.IsNullOrEmpty(this.key))
		{
			this.value = Localization.Get(this.key);
		}
	}

	// Token: 0x0400079A RID: 1946
	public string key;

	// Token: 0x0400079B RID: 1947
	private bool mStarted;
}
