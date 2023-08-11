using UnityEngine;
using UnityEngine.UI;

namespace Fight;

public class FightBuffCell : MonoBehaviour
{
	[SerializeField]
	private Text NumText;

	[SerializeField]
	private Image Icon;

	[SerializeField]
	private int buffCount;

	public int Id;

	public string Desc;

	public int BuffCount
	{
		get
		{
			return buffCount;
		}
		set
		{
			buffCount = value;
			NumText.text = buffCount.ToString();
		}
	}

	public void Init(int id, int count, string desc, Sprite sprite = null)
	{
		Id = id;
		BuffCount = count;
		Desc = desc;
		if ((Object)(object)sprite != (Object)null)
		{
			Icon.sprite = sprite;
		}
	}
}
