using UnityEngine;
using UnityEngine.SceneManagement;

public class CyUIMag : MonoBehaviour, IESCClose
{
	public static CyUIMag inst;

	public CyNpcList npcList;

	public CyPaiMaiPanel PaiMaiPanel;

	public CyEmail cyEmail;

	public GameObject No;

	private void Awake()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		SceneManager.activeSceneChanged += delegate
		{
			Close();
		};
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		inst = this;
	}

	private void Start()
	{
		npcList.Init();
		No.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Close()
	{
		if (!((Object)(object)inst == (Object)null))
		{
			UIHeadPanel.Inst.CheckHongDian(checkChuanYin: true);
			PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符);
			ESCCloseManager.Inst.UnRegisterClose(this);
		}
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
