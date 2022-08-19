using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011C RID: 284
public class UICanvasManager : MonoBehaviour
{
	// Token: 0x06000D8F RID: 3471 RVA: 0x000512BF File Offset: 0x0004F4BF
	private void Awake()
	{
		UICanvasManager.GlobalAccess = this;
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000512C7 File Offset: 0x0004F4C7
	private void Start()
	{
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000512EC File Offset: 0x0004F4EC
	private void Update()
	{
		if (!this.MouseOverButton && Input.GetMouseButtonUp(0))
		{
			this.SpawnCurrentParticleEffect();
		}
		if (Input.GetKeyUp(97))
		{
			this.SelectPreviousPE();
		}
		if (Input.GetKeyUp(100))
		{
			this.SelectNextPE();
		}
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x00051322 File Offset: 0x0004F522
	public void UpdateToolTip(ButtonTypes toolTipType)
	{
		if (this.ToolTipText != null)
		{
			if (toolTipType == ButtonTypes.Previous)
			{
				this.ToolTipText.text = "Select Previous Particle Effect";
				return;
			}
			if (toolTipType == ButtonTypes.Next)
			{
				this.ToolTipText.text = "Select Next Particle Effect";
			}
		}
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0005135B File Offset: 0x0004F55B
	public void ClearToolTip()
	{
		if (this.ToolTipText != null)
		{
			this.ToolTipText.text = "";
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0005137B File Offset: 0x0004F57B
	private void SelectPreviousPE()
	{
		ParticleEffectsLibrary.GlobalAccess.PreviousParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x000513AA File Offset: 0x0004F5AA
	private void SelectNextPE()
	{
		ParticleEffectsLibrary.GlobalAccess.NextParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x000513D9 File Offset: 0x0004F5D9
	private void SpawnCurrentParticleEffect()
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), ref this.rayHit))
		{
			ParticleEffectsLibrary.GlobalAccess.SpawnParticleEffect(this.rayHit.point);
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0005140C File Offset: 0x0004F60C
	public void UIButtonClick(ButtonTypes buttonTypeClicked)
	{
		if (buttonTypeClicked == ButtonTypes.Previous)
		{
			this.SelectPreviousPE();
			return;
		}
		if (buttonTypeClicked != ButtonTypes.Next)
		{
			return;
		}
		this.SelectNextPE();
	}

	// Token: 0x04000995 RID: 2453
	public static UICanvasManager GlobalAccess;

	// Token: 0x04000996 RID: 2454
	public bool MouseOverButton;

	// Token: 0x04000997 RID: 2455
	public Text PENameText;

	// Token: 0x04000998 RID: 2456
	public Text ToolTipText;

	// Token: 0x04000999 RID: 2457
	private RaycastHit rayHit;
}
