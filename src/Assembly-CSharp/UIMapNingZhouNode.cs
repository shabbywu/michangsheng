using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMapNingZhouNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public string NodeName;

	public string WarpSceneName;

	private Image iconImage;

	private Text nodeNameText;

	private Vector3 imageOriScale;

	private Color textOriColor;

	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);

	public void SetNodeName(string nodeName)
	{
		NodeName = nodeName;
		nodeNameText.text = nodeName;
	}

	public void Init()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		iconImage = ((Component)((Component)this).transform.GetChild(0)).GetComponent<Image>();
		nodeNameText = ((Component)((Component)this).transform.GetChild(1)).GetComponent<Text>();
		imageOriScale = ((Component)iconImage).transform.localScale;
		textOriColor = ((Graphic)nodeNameText).color;
	}

	public void SetCanJiaoHu(bool can)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)iconImage).transform.localScale = imageOriScale;
		((Graphic)iconImage).raycastTarget = can;
	}

	public void SetNodeAlpha(bool alpha)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (alpha)
		{
			((Graphic)iconImage).color = new Color(1f, 1f, 1f, 0.5f);
			((Graphic)nodeNameText).color = textAnColor;
		}
		else
		{
			((Graphic)iconImage).color = Color.white;
			((Graphic)nodeNameText).color = textOriColor;
		}
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		UIMapPanel.Inst.NingZhou.OnNodeClick(this);
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		((Component)iconImage).transform.localScale = imageOriScale * 1.2f;
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)iconImage).transform.localScale = imageOriScale;
	}
}
