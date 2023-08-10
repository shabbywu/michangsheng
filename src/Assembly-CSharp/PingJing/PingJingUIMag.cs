using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace PingJing;

public class PingJingUIMag : MonoBehaviour
{
	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Transform Panel;

	public void Show()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		Desc.text = "\u3000\u3000体内真元逐渐饱和，你的修为已经达到了<color=#ffface>" + LevelUpDataJsonData.DataDict[Tools.instance.getPlayer().level].Name + "</color>的瓶颈，如果无法突破，就再难提升了";
		Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(Panel, Vector3.one, 0.5f);
	}

	public void Close()
	{
		Tools.canClickFlag = true;
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)27))
		{
			Close();
		}
	}
}
