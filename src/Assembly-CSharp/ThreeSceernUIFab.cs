using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class ThreeSceernUIFab : MonoBehaviour
{
	// Token: 0x06001E3B RID: 7739 RVA: 0x000D5496 File Offset: 0x000D3696
	private void Start()
	{
		ThreeSceernUIFab.inst = this;
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x000D54A0 File Offset: 0x000D36A0
	public void FixPostion()
	{
		if (this.uIWidget == null)
		{
			this.uIWidget = base.GetComponent<UIWidget>();
		}
		this.uIWidget.enabled = true;
		this.uIWidget.SetAnchor(UI_Manager.inst.gameObject);
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnUpdate;
		this.uIWidget.leftAnchor.relative = 1f;
		this.uIWidget.rightAnchor.relative = 1f;
		this.uIWidget.bottomAnchor.relative = 0f;
		this.uIWidget.topAnchor.relative = 0f;
		this.uIWidget.leftAnchor.absolute = 50;
		this.uIWidget.rightAnchor.absolute = 100;
		this.uIWidget.bottomAnchor.absolute = 66;
		this.uIWidget.topAnchor.absolute = 116;
		base.Invoke("lateAction", 1f);
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x000D55A0 File Offset: 0x000D37A0
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x040018D1 RID: 6353
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x040018D2 RID: 6354
	public static ThreeSceernUIFab inst;
}
