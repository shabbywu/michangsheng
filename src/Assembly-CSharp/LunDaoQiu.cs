using UnityEngine;
using UnityEngine.UI;

public class LunDaoQiu : MonoBehaviour
{
	public Image lunDaoQiuImage;

	[SerializeField]
	public Text curLevel;

	public int wudaoId;

	public int level;

	public bool isNull;

	public void SetNull()
	{
		((Component)lunDaoQiuImage).gameObject.SetActive(false);
		wudaoId = -1;
		isNull = true;
		level = 0;
	}

	public void SetData(int id, int curLevel)
	{
		isNull = false;
		wudaoId = id;
		level = curLevel;
		this.curLevel.text = curLevel.ToString();
		lunDaoQiuImage.sprite = LunDaoManager.inst.lunDaoPanel.wuDaoQiuSpriteList[id];
		((Component)lunDaoQiuImage).gameObject.SetActive(true);
	}

	public LunDaoQiu LevelUp()
	{
		level++;
		curLevel.text = level.ToString();
		return this;
	}
}
