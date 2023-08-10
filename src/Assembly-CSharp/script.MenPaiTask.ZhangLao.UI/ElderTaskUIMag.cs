using UnityEngine;
using script.MenPaiTask.ZhangLao.UI.Base;
using script.MenPaiTask.ZhangLao.UI.UI;

namespace script.MenPaiTask.ZhangLao.UI;

public class ElderTaskUIMag : MonoBehaviour, IESCClose
{
	public static ElderTaskUIMag Inst;

	public CreateElderTaskUI CreateElderTaskUI;

	public ElderTaskBag Bag;

	public ElderTaskUI ElderTaskUI;

	public static void Open()
	{
		ResManager.inst.LoadPrefab("ElderTaskUI").Inst(((Component)NewUICanvas.Inst).transform);
	}

	private void Awake()
	{
		Inst = this;
		CreateElderTaskUI = new CreateElderTaskUI(((Component)((Component)this).transform.Find("发布任务界面")).gameObject);
		ElderTaskUI = new ElderTaskUI(((Component)((Component)this).transform.Find("任务浏览界面")).gameObject);
		Bag = ((Component)((Component)this).transform.Find("背包")).GetComponent<ElderTaskBag>();
		ESCCloseManager.Inst.RegisterClose(Inst);
		OpenElderTaskUI();
	}

	public void OpenCreateElderTaskUI()
	{
		if (!CreateElderTaskUI.IsActive())
		{
			CreateElderTaskUI.Show();
			ElderTaskUI.Hide();
		}
	}

	public void OpenElderTaskUI()
	{
		if (!ElderTaskUI.IsActive())
		{
			ElderTaskUI.Show();
			CreateElderTaskUI.Hide();
		}
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ESCCloseManager.Inst.UnRegisterClose(Inst);
		Inst = null;
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
