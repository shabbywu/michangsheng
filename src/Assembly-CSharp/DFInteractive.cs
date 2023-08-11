using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class DFInteractive : MonoBehaviour
{
	public Material NormalMat;

	public Material HighlightMat;

	public Color HighlightColor = Color.white;

	public UnityEvent OnFunctionClick;

	public UnityEvent OnDecorateClick;

	private SpriteRenderer sr;

	private bool isLight;

	private DFInteractiveMode lastMode;

	private bool IsLight
	{
		get
		{
			return isLight;
		}
		set
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			isLight = value;
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function)
			{
				if (isLight)
				{
					((Renderer)sr).material = HighlightMat;
					((Renderer)sr).material.SetColor("_LtColor", HighlightColor);
				}
				else
				{
					((Renderer)sr).material = NormalMat;
				}
			}
			else if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate)
			{
				if (isLight)
				{
					((Renderer)sr).material = HighlightMat;
					((Renderer)sr).material.SetColor("_LtColor", HighlightColor);
				}
				else
				{
					((Renderer)sr).material = HighlightMat;
					((Renderer)sr).material.SetColor("_LtColor", Color.white);
				}
			}
		}
	}

	private void Awake()
	{
		sr = ((Component)this).GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (lastMode != DongFuScene.Inst.InteractiveMode)
		{
			IsLight = false;
			lastMode = DongFuScene.Inst.InteractiveMode;
		}
	}

	private void OnMouseEnter()
	{
		if (Tools.instance.canClick())
		{
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function && OnFunctionClick != null)
			{
				IsLight = true;
			}
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate && OnDecorateClick != null)
			{
				IsLight = true;
			}
		}
	}

	private void OnMouseExit()
	{
		IsLight = false;
	}

	private void OnMouseUp()
	{
		if (Tools.instance.canClick() && PanelMamager.inst.nowPanel == PanelMamager.PanelType.空)
		{
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Function && OnFunctionClick != null)
			{
				OnFunctionClick.Invoke();
			}
			if (DongFuScene.Inst.InteractiveMode == DFInteractiveMode.Decorate && OnDecorateClick != null)
			{
				OnDecorateClick.Invoke();
			}
		}
	}
}
