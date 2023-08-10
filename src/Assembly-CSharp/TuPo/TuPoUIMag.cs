using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TuPo;

public class TuPoUIMag : MonoBehaviour, IESCClose
{
	[SerializeField]
	private List<Sprite> SpritesList;

	[SerializeField]
	private Image LevelImage;

	[SerializeField]
	private Text OldHP;

	[SerializeField]
	private Text CurHP;

	[SerializeField]
	private Text OldShenShi;

	[SerializeField]
	private Text CurShenShi;

	[SerializeField]
	private Text OldShouYuan;

	[SerializeField]
	private Text CurShouYuan;

	[SerializeField]
	private Text OldDunSu;

	[SerializeField]
	private Text CurDunSu;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private Transform Panel;

	private bool loadTalk4031OnClose;

	public void ShowTuPo(int level, int oldHp, int curHp, int oldShenShi, int curShenShi, int oldShouYuan, int curShouYuan, int oldDunSu, int curDunSu, bool isBigTuPo, string desc)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		if (BindData.ContainsKey("FightBeforeHpMax"))
		{
			oldHp = BindData.Get<int>("FightBeforeHpMax");
		}
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = new Vector3(0f, 81f, 0f);
		((Component)this).transform.localScale = new Vector3(0.821f, 0.821f, 0.821f);
		((Component)this).transform.SetAsLastSibling();
		Tools.canClickFlag = false;
		LevelImage.sprite = SpritesList[level - 1];
		OldHP.text = oldHp.ToString();
		CurHP.text = curHp.ToString();
		OldShenShi.text = oldShenShi.ToString();
		CurShenShi.text = curShenShi.ToString();
		OldShouYuan.text = oldShouYuan.ToString();
		CurShouYuan.text = curShouYuan.ToString();
		OldDunSu.text = oldDunSu.ToString();
		CurDunSu.text = curDunSu.ToString();
		Desc.text = desc;
		((Component)this).gameObject.SetActive(true);
		Panel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(Panel, Vector3.one, 0.5f);
		ESCCloseManager.Inst.RegisterClose(this);
		if (level == 13)
		{
			loadTalk4031OnClose = true;
		}
	}

	public void Close()
	{
		if (loadTalk4031OnClose)
		{
			Resources.Load<GameObject>("talkPrefab/TalkPrefab/Talk4031").Inst();
		}
		Tools.canClickFlag = true;
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
