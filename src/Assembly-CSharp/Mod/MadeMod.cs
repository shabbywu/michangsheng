using UnityEngine;

namespace Mod;

public class MadeMod : MonoBehaviour, IESCClose
{
	public static MadeMod Inst;

	public Transform Transform;

	private void Awake()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Inst = this;
		Transform = ((Component)this).transform;
		Transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		Transform.localPosition = Vector3.zero;
		Transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		PanelMamager.CanOpenOrClose = false;
	}

	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		Inst = null;
	}

	public void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		((Component)this).gameObject.SetActive(true);
	}

	public void Close()
	{
		Tools.canClickFlag = true;
		PanelMamager.CanOpenOrClose = true;
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
