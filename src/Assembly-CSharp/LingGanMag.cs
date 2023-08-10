using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class LingGanMag : MonoBehaviour
{
	public static LingGanMag inst;

	[SerializeField]
	private List<Sprite> sprites;

	private List<string> colors;

	public UILabel curState;

	public UI2DSprite sprite;

	public UILabel num;

	private void Awake()
	{
		inst = this;
		colors = new List<string>();
		colors.Add("8df1ec");
		colors.Add("64c97a");
		colors.Add("e9e4a4");
		colors.Add("aaab96");
	}

	public void UpdateData()
	{
		Avatar player = Tools.instance.getPlayer();
		curState.text = "[" + colors[player.LunDaoState - 1] + "]" + jsonData.instance.LunDaoStateData[player.LunDaoState.ToString()]["ZhuangTaiInfo"].Str;
		num.text = $"{player.LingGan}/{player.GetLingGanMax()}";
		sprite.sprite2D = sprites[player.LunDaoState - 1];
	}
}
