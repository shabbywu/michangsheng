using System;
using System.Runtime.CompilerServices;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using script.SetFace;

public class SetFaceUI : MonoBehaviour, IESCClose
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__5_1;

		internal void _003COpen_003Eb__5_1()
		{
			ResManager.inst.LoadPrefab("SetFaceUI").Inst(((Component)NewUICanvas.Inst).transform);
		}
	}

	public static SetFaceUI Inst;

	public MainUISetFaceB SetFaceB;

	public PlayerSetRandomFace Face;

	public JSONObject OldFaceData;

	public GameObject Lock;

	public static void Open()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		UnityAction val = delegate
		{
			if (player.hasItem(5322))
			{
				Tools.instance.RemoveItem(5322);
				player.IsCanSetFace = true;
				ResManager.inst.LoadPrefab("SetFaceUI").Inst(((Component)NewUICanvas.Inst).transform);
			}
			else
			{
				UIPopTip.Inst.Pop("易容丹数量不足");
			}
		};
		object obj = _003C_003Ec._003C_003E9__5_1;
		if (obj == null)
		{
			UnityAction val2 = delegate
			{
				ResManager.inst.LoadPrefab("SetFaceUI").Inst(((Component)NewUICanvas.Inst).transform);
			};
			_003C_003Ec._003C_003E9__5_1 = val2;
			obj = (object)val2;
		}
		USelectBox.Show("是否要服用易容丹", val, (UnityAction)obj);
	}

	private void Awake()
	{
		Lock.SetActive(!Tools.instance.getPlayer().IsCanSetFace);
		if (Tools.instance.getPlayer().IsCanSetFace)
		{
			Tools.instance.getPlayer().IsCanSetFace = false;
		}
		ESCCloseManager.Inst.RegisterClose(this);
		Inst = this;
		((Component)this).transform.SetAsLastSibling();
		OldFaceData = jsonData.instance.AvatarRandomJsonData[1.ToString()].Copy();
		AvatarFaceDatabase.inst.ListType = GetSex();
		SetFaceB.Init();
	}

	public int GetSex()
	{
		return jsonData.instance.AvatarRandomJsonData[1.ToString()]["Sex"].I;
	}

	private void OnDestroy()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Inst = null;
	}

	public void Cancel()
	{
		jsonData.instance.AvatarRandomJsonData.SetField("1", OldFaceData);
		Close();
	}

	public void Ok()
	{
		Close();
	}

	public void Close()
	{
		UIHeadPanel.Inst.Face.SetNPCFace(1);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public bool TryEscClose()
	{
		Cancel();
		return true;
	}
}
