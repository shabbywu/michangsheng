using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003C2 RID: 962
public class CyUIMag : MonoBehaviour, IESCClose
{
	// Token: 0x06001A8C RID: 6796 RVA: 0x000EA774 File Offset: 0x000E8974
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

	// Token: 0x06001A8D RID: 6797 RVA: 0x00016971 File Offset: 0x00014B71
	private void Start()
	{
		this.npcList.Init();
		this.No.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x00016995 File Offset: 0x00014B95
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

	// Token: 0x06001A8F RID: 6799 RVA: 0x000169C7 File Offset: 0x00014BC7
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040015FF RID: 5631
	public static CyUIMag inst;

	// Token: 0x04001600 RID: 5632
	public CyNpcList npcList;

	// Token: 0x04001601 RID: 5633
	public CyPaiMaiPanel PaiMaiPanel;

	// Token: 0x04001602 RID: 5634
	public CyEmail cyEmail;

	// Token: 0x04001603 RID: 5635
	public GameObject No;
}
