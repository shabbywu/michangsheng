using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000317 RID: 791
[RequireComponent(typeof(SpriteRenderer))]
public class DFInteractive : MonoBehaviour
{
	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06001763 RID: 5987 RVA: 0x00014A85 File Offset: 0x00012C85
	// (set) Token: 0x06001764 RID: 5988 RVA: 0x000CD788 File Offset: 0x000CB988
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

	// Token: 0x06001765 RID: 5989 RVA: 0x00014A8D File Offset: 0x00012C8D
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x00014A9B File Offset: 0x00012C9B
	private void Update()
	{
		if (this.lastMode != DongFuScene.Inst.InteractiveMode)
		{
			this.IsLight = false;
			this.lastMode = DongFuScene.Inst.InteractiveMode;
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000CD85C File Offset: 0x000CBA5C
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

	// Token: 0x06001768 RID: 5992 RVA: 0x00014AC6 File Offset: 0x00012CC6
	private void OnMouseExit()
	{
		this.IsLight = false;
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000CD8B0 File Offset: 0x000CBAB0
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

	// Token: 0x040012BA RID: 4794
	public Material NormalMat;

	// Token: 0x040012BB RID: 4795
	public Material HighlightMat;

	// Token: 0x040012BC RID: 4796
	public Color HighlightColor = Color.white;

	// Token: 0x040012BD RID: 4797
	public UnityEvent OnFunctionClick;

	// Token: 0x040012BE RID: 4798
	public UnityEvent OnDecorateClick;

	// Token: 0x040012BF RID: 4799
	private SpriteRenderer sr;

	// Token: 0x040012C0 RID: 4800
	private bool isLight;

	// Token: 0x040012C1 RID: 4801
	private DFInteractiveMode lastMode;
}
