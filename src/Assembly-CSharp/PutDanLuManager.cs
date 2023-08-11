using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PutDanLuManager : MonoBehaviour
{
	[SerializeField]
	private Button putDanLuBtn;

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)putDanLuBtn.onClick).AddListener(new UnityAction(putDanLu));
	}

	public void putDanLu()
	{
		LianDanSystemManager.inst.lianDanPageManager.CanClick = false;
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.setCurSelectIndex(30);
		LianDanSystemManager.inst.selectLianDanCaiLiaoPage.openCaiLiaoPackge(9);
	}

	public void backPutDanLuPanel()
	{
		((Component)this).gameObject.SetActive(true);
		((Component)LianDanSystemManager.inst.lianDanPageManager).gameObject.SetActive(false);
	}
}
