using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DE RID: 478
public class UICanvasManager : MonoBehaviour
{
	// Token: 0x06000F68 RID: 3944 RVA: 0x0000FA1B File Offset: 0x0000DC1B
	private void Awake()
	{
		UICanvasManager.GlobalAccess = this;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0000FA23 File Offset: 0x0000DC23
	private void Start()
	{
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0000FA48 File Offset: 0x0000DC48
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

	// Token: 0x06000F6B RID: 3947 RVA: 0x0000FA7E File Offset: 0x0000DC7E
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

	// Token: 0x06000F6C RID: 3948 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
	public void ClearToolTip()
	{
		if (this.ToolTipText != null)
		{
			this.ToolTipText.text = "";
		}
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
	private void SelectPreviousPE()
	{
		ParticleEffectsLibrary.GlobalAccess.PreviousParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0000FB06 File Offset: 0x0000DD06
	private void SelectNextPE()
	{
		ParticleEffectsLibrary.GlobalAccess.NextParticleEffect();
		if (this.PENameText != null)
		{
			this.PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x0000FB35 File Offset: 0x0000DD35
	private void SpawnCurrentParticleEffect()
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), ref this.rayHit))
		{
			ParticleEffectsLibrary.GlobalAccess.SpawnParticleEffect(this.rayHit.point);
		}
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x0000FB68 File Offset: 0x0000DD68
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

	// Token: 0x04000C19 RID: 3097
	public static UICanvasManager GlobalAccess;

	// Token: 0x04000C1A RID: 3098
	public bool MouseOverButton;

	// Token: 0x04000C1B RID: 3099
	public Text PENameText;

	// Token: 0x04000C1C RID: 3100
	public Text ToolTipText;

	// Token: 0x04000C1D RID: 3101
	private RaycastHit rayHit;
}
