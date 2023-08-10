using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMapSeaNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public string WarpSceneName;

	public string NodeName;

	public int NodeIndex;

	public int AccessStaticValueID;

	public bool AlwaysShow;

	private Image iconImage;

	private Text nodeNameText;

	private Vector3 imageOriScale;

	private Color textOriColor;

	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);

	public bool IsAccessed;

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

	public void RefreshUI()
	{
		if (AlwaysShow)
		{
			IsAccessed = true;
		}
		else if (PlayerEx.Player != null)
		{
			IsAccessed = GlobalValue.Get(AccessStaticValueID, "UIMapSeaNode.RefreshUI 判断是否访问过岛屿") == 1;
		}
		else
		{
			IsAccessed = true;
		}
		if (IsAccessed)
		{
			nodeNameText.text = NodeName;
		}
		else
		{
			nodeNameText.text = "???";
		}
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (IsAccessed)
		{
			UIMapPanel.Inst.Sea.OnNodeClick(this);
		}
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (IsAccessed)
		{
			((Component)iconImage).transform.localScale = imageOriScale * 1.2f;
		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (IsAccessed)
		{
			((Component)iconImage).transform.localScale = imageOriScale;
		}
	}
}
