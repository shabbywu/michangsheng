using UnityEngine;
using UnityEngine.Events;

public class UIJianLingPanel : MonoBehaviour, IESCClose
{
	public static UIJianLingPanel Inst;

	public FpBtn JiaoTanBtn;

	public FpBtn XianSuoBtn;

	public FpBtn QingJiaoBtn;

	public FpBtn LiKaiBtn;

	public GameObject CenterObj;

	public GameObject QingJiaoRedPoint;

	private void Awake()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		Inst = this;
		JiaoTanBtn.mouseUpEvent.AddListener(new UnityAction(OnJiaoTanBtnClick));
		XianSuoBtn.mouseUpEvent.AddListener(new UnityAction(OnXianSuoBtnClick));
		QingJiaoBtn.mouseUpEvent.AddListener(new UnityAction(OnQingJiaoBtnClick));
		LiKaiBtn.mouseUpEvent.AddListener(new UnityAction(OnLiKaiBtnClick));
		ESCCloseManager.Inst.RegisterClose(this);
	}

	private void Update()
	{
		if ((Object)(object)UIHeadPanel.Inst != (Object)null && QingJiaoRedPoint.activeSelf != UIHeadPanel.Inst.TieJianRedPoint.activeSelf)
		{
			QingJiaoRedPoint.SetActive(UIHeadPanel.Inst.TieJianRedPoint.activeSelf);
		}
	}

	public static void OpenPanel()
	{
		int num = GlobalValue.Get(692, "打开剑灵界面");
		if (num == 3 || num == 4)
		{
			NPCEx.OpenTalk(518);
		}
		else
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingPanel"), ((Component)NewUICanvas.Inst.Canvas).transform).transform.SetAsLastSibling();
		}
	}

	bool IESCClose.TryEscClose()
	{
		Close();
		return true;
	}

	public void Close()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void OnJiaoTanBtnClick()
	{
		NPCEx.OpenTalk(518);
		CenterObj.SetActive(false);
	}

	public void OnJiaoTanEnd()
	{
		CenterObj.SetActive(true);
	}

	public void OnXianSuoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingXianSuoPanel"), ((Component)NewUICanvas.Inst.Canvas).transform).transform.SetAsLastSibling();
		Close();
	}

	public void OnQingJiaoBtnClick()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/JianLing/UIJianLingQingJiaoPanel"), ((Component)NewUICanvas.Inst.Canvas).transform).transform.SetAsLastSibling();
		Close();
	}

	public void OnLiKaiBtnClick()
	{
		Close();
	}
}
