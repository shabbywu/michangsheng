using System;
using KBEngine;
using script.SetFace;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class SetFaceUI : MonoBehaviour, IESCClose
{
	// Token: 0x06001F4A RID: 8010 RVA: 0x000DC164 File Offset: 0x000DA364
	public static void Open()
	{
		Avatar player = Tools.instance.getPlayer();
		USelectBox.Show("是否要服用易容丹", delegate
		{
			if (player.hasItem(5322))
			{
				Tools.instance.RemoveItem(5322, 1);
				player.IsCanSetFace = true;
				ResManager.inst.LoadPrefab("SetFaceUI").Inst(NewUICanvas.Inst.transform);
				return;
			}
			UIPopTip.Inst.Pop("易容丹数量不足", PopTipIconType.叹号);
		}, delegate
		{
			ResManager.inst.LoadPrefab("SetFaceUI").Inst(NewUICanvas.Inst.transform);
		});
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x000DC1BC File Offset: 0x000DA3BC
	private void Awake()
	{
		this.Lock.SetActive(!Tools.instance.getPlayer().IsCanSetFace);
		if (Tools.instance.getPlayer().IsCanSetFace)
		{
			Tools.instance.getPlayer().IsCanSetFace = false;
		}
		ESCCloseManager.Inst.RegisterClose(this);
		SetFaceUI.Inst = this;
		base.transform.SetAsLastSibling();
		this.OldFaceData = jsonData.instance.AvatarRandomJsonData[1.ToString()].Copy();
		AvatarFaceDatabase.inst.ListType = this.GetSex();
		this.SetFaceB.Init();
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x000DC264 File Offset: 0x000DA464
	public int GetSex()
	{
		return jsonData.instance.AvatarRandomJsonData[1.ToString()]["Sex"].I;
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x000DC298 File Offset: 0x000DA498
	private void OnDestroy()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		SetFaceUI.Inst = null;
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x000DC2AB File Offset: 0x000DA4AB
	public void Cancel()
	{
		jsonData.instance.AvatarRandomJsonData.SetField("1", this.OldFaceData);
		this.Close();
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x000DC2CD File Offset: 0x000DA4CD
	public void Ok()
	{
		this.Close();
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x000DC2D5 File Offset: 0x000DA4D5
	public void Close()
	{
		UIHeadPanel.Inst.Face.SetNPCFace(1);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x000DC2F2 File Offset: 0x000DA4F2
	public bool TryEscClose()
	{
		this.Cancel();
		return true;
	}

	// Token: 0x04001962 RID: 6498
	public static SetFaceUI Inst;

	// Token: 0x04001963 RID: 6499
	public MainUISetFaceB SetFaceB;

	// Token: 0x04001964 RID: 6500
	public PlayerSetRandomFace Face;

	// Token: 0x04001965 RID: 6501
	public JSONObject OldFaceData;

	// Token: 0x04001966 RID: 6502
	public GameObject Lock;
}
