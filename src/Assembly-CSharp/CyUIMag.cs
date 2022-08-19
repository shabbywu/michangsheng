using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000293 RID: 659
public class CyUIMag : MonoBehaviour, IESCClose
{
	// Token: 0x060017AF RID: 6063 RVA: 0x000A3564 File Offset: 0x000A1764
	private void Awake()
	{
		SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
		{
			this.Close();
		};
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		CyUIMag.inst = this;
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000A35CD File Offset: 0x000A17CD
	private void Start()
	{
		this.npcList.Init();
		this.No.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000A35F1 File Offset: 0x000A17F1
	public void Close()
	{
		if (CyUIMag.inst == null)
		{
			return;
		}
		UIHeadPanel.Inst.CheckHongDian(true);
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 0);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000A3623 File Offset: 0x000A1823
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001279 RID: 4729
	public static CyUIMag inst;

	// Token: 0x0400127A RID: 4730
	public CyNpcList npcList;

	// Token: 0x0400127B RID: 4731
	public CyPaiMaiPanel PaiMaiPanel;

	// Token: 0x0400127C RID: 4732
	public CyEmail cyEmail;

	// Token: 0x0400127D RID: 4733
	public GameObject No;
}
