using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TpUIMag : MonoBehaviour
{
	public static TpUIMag inst;

	[SerializeField]
	private GameObject tpPanel;

	public TpEatDanYao tpEatDanYao;

	public UnityAction call;

	private void Awake()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		inst = this;
	}

	public void Init()
	{
		tpPanel.SetActive(true);
		((Component)tpEatDanYao).gameObject.SetActive(false);
	}

	public void OpenEatDanYaoPanel()
	{
		tpPanel.SetActive(false);
		tpEatDanYao.Init();
	}

	public void StartTupo()
	{
		call.Invoke();
		Close();
	}

	public void GiveUp()
	{
		GlobalValue.SetTalk(1, 0, "TpUIMag.GiveUp");
		Tools.instance.monstarMag.monstarAddBuff = new Dictionary<int, int>();
		Tools.instance.monstarMag.HeroAddBuff = new Dictionary<int, int>();
		Close();
	}

	private void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void ShowTuPoPanel()
	{
		tpPanel.SetActive(true);
	}
}
