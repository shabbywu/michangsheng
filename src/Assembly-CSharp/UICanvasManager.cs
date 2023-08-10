using UnityEngine;
using UnityEngine.UI;

public class UICanvasManager : MonoBehaviour
{
	public static UICanvasManager GlobalAccess;

	public bool MouseOverButton;

	public Text PENameText;

	public Text ToolTipText;

	private RaycastHit rayHit;

	private void Awake()
	{
		GlobalAccess = this;
	}

	private void Start()
	{
		if ((Object)(object)PENameText != (Object)null)
		{
			PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	private void Update()
	{
		if (!MouseOverButton && Input.GetMouseButtonUp(0))
		{
			SpawnCurrentParticleEffect();
		}
		if (Input.GetKeyUp((KeyCode)97))
		{
			SelectPreviousPE();
		}
		if (Input.GetKeyUp((KeyCode)100))
		{
			SelectNextPE();
		}
	}

	public void UpdateToolTip(ButtonTypes toolTipType)
	{
		if ((Object)(object)ToolTipText != (Object)null)
		{
			switch (toolTipType)
			{
			case ButtonTypes.Previous:
				ToolTipText.text = "Select Previous Particle Effect";
				break;
			case ButtonTypes.Next:
				ToolTipText.text = "Select Next Particle Effect";
				break;
			}
		}
	}

	public void ClearToolTip()
	{
		if ((Object)(object)ToolTipText != (Object)null)
		{
			ToolTipText.text = "";
		}
	}

	private void SelectPreviousPE()
	{
		ParticleEffectsLibrary.GlobalAccess.PreviousParticleEffect();
		if ((Object)(object)PENameText != (Object)null)
		{
			PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	private void SelectNextPE()
	{
		ParticleEffectsLibrary.GlobalAccess.NextParticleEffect();
		if ((Object)(object)PENameText != (Object)null)
		{
			PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
		}
	}

	private void SpawnCurrentParticleEffect()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), ref rayHit))
		{
			ParticleEffectsLibrary.GlobalAccess.SpawnParticleEffect(((RaycastHit)(ref rayHit)).point);
		}
	}

	public void UIButtonClick(ButtonTypes buttonTypeClicked)
	{
		switch (buttonTypeClicked)
		{
		case ButtonTypes.Previous:
			SelectPreviousPE();
			break;
		case ButtonTypes.Next:
			SelectNextPE();
			break;
		}
	}
}
