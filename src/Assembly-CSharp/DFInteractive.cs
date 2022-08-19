using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000202 RID: 514
[RequireComponent(typeof(SpriteRenderer))]
public class DFInteractive : MonoBehaviour
{
	// Token: 0x17000237 RID: 567
	// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00084D1E File Offset: 0x00082F1E
	// (set) Token: 0x060014BA RID: 5306 RVA: 0x00084D28 File Offset: 0x00082F28
	private bool IsLight
	{
		get
		{
			return this.isLight;
		}
		set
		{
			this.isLight = value;
			if (DongFuScene.Inst.InteractiveMode != DFInteractiveMode.Function)
			{
				if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate)
				{
					if (this.isLight)
					{
						this.sr.material = this.HighlightMat;
						this.sr.material.SetColor("_LtColor", this.HighlightColor);
						return;
					}
					this.sr.material = this.HighlightMat;
					this.sr.material.SetColor("_LtColor", Color.white);
				}
				return;
			}
			if (this.isLight)
			{
				this.sr.material = this.HighlightMat;
				this.sr.material.SetColor("_LtColor", this.HighlightColor);
				return;
			}
			this.sr.material = this.NormalMat;
		}
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x00084DFC File Offset: 0x00082FFC
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x00084E0A File Offset: 0x0008300A
	private void Update()
	{
		if (this.lastMode != DongFuScene.Inst.InteractiveMode)
		{
			this.IsLight = false;
			this.lastMode = DongFuScene.Inst.InteractiveMode;
		}
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x00084E38 File Offset: 0x00083038
	private void OnMouseEnter()
	{
		if (Tools.instance.canClick(false, true))
		{
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function && this.OnFunctionClick != null)
			{
				this.IsLight = true;
			}
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate && this.OnDecorateClick != null)
			{
				this.IsLight = true;
			}
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x00084E8A File Offset: 0x0008308A
	private void OnMouseExit()
	{
		this.IsLight = false;
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x00084E94 File Offset: 0x00083094
	private void OnMouseUp()
	{
		if (Tools.instance.canClick(false, true) && PanelMamager.inst.nowPanel == PanelMamager.PanelType.空)
		{
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function && this.OnFunctionClick != null)
			{
				this.OnFunctionClick.Invoke();
			}
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate && this.OnDecorateClick != null)
			{
				this.OnDecorateClick.Invoke();
			}
		}
	}

	// Token: 0x04000F74 RID: 3956
	public Material NormalMat;

	// Token: 0x04000F75 RID: 3957
	public Material HighlightMat;

	// Token: 0x04000F76 RID: 3958
	public Color HighlightColor = Color.white;

	// Token: 0x04000F77 RID: 3959
	public UnityEvent OnFunctionClick;

	// Token: 0x04000F78 RID: 3960
	public UnityEvent OnDecorateClick;

	// Token: 0x04000F79 RID: 3961
	private SpriteRenderer sr;

	// Token: 0x04000F7A RID: 3962
	private bool isLight;

	// Token: 0x04000F7B RID: 3963
	private DFInteractiveMode lastMode;
}
