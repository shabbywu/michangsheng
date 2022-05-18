using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000522 RID: 1314
public class ThreeSceernUIFab : MonoBehaviour
{
	// Token: 0x060021BC RID: 8636 RVA: 0x0001BBAF File Offset: 0x00019DAF
	private void Start()
	{
		ThreeSceernUIFab.inst = this;
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x00118E94 File Offset: 0x00117094
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

	// Token: 0x060021BE RID: 8638 RVA: 0x0001BBB7 File Offset: 0x00019DB7
	public void lateAction()
	{
		this.uIWidget.updateAnchors = UIRect.AnchorUpdate.OnEnable;
	}

	// Token: 0x04001D3A RID: 7482
	[SerializeField]
	private UIWidget uIWidget;

	// Token: 0x04001D3B RID: 7483
	public static ThreeSceernUIFab inst;
}
