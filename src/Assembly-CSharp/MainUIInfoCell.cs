using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUIInfoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Text text;

	public int baseNum;

	public int curNum;

	public string desc;

	public void UpdateNum(string content, bool isLinGen = false)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		text.text = content;
		if (!isLinGen)
		{
			if (curNum > baseNum)
			{
				((Graphic)text).color = new Color(0.7921569f, 83f / 85f, 21f / 85f);
			}
			else if (curNum < baseNum)
			{
				((Graphic)text).color = new Color(1f, 64f / 85f, 0.6313726f);
			}
			else
			{
				((Graphic)text).color = new Color(1f, 84f / 85f, 43f / 51f);
			}
		}
		else if (curNum > baseNum)
		{
			((Graphic)text).color = new Color(1f, 64f / 85f, 0.6313726f);
		}
		else if (curNum < baseNum)
		{
			((Graphic)text).color = new Color(0.7921569f, 83f / 85f, 21f / 85f);
		}
		else
		{
			((Graphic)text).color = new Color(1f, 84f / 85f, 43f / 51f);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		MainUIMag.inst.tooltip.Show(desc, new Vector3(((Component)this).transform.position.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z));
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}
}
