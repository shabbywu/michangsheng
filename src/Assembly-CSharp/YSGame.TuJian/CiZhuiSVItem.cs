using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class CiZhuiSVItem : MonoBehaviour
{
	[HideInInspector]
	public int ID;

	[HideInInspector]
	public float Height;

	public Text _TitleText;

	public SymbolText _HyText;

	private RectTransform _RT;

	private void Awake()
	{
		ref RectTransform rT = ref _RT;
		Transform transform = ((Component)this).transform;
		rT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
	}

	private void Update()
	{
		RefreshHeight();
	}

	public void SetCiZhui(int id, string name, string desc)
	{
		ID = id;
		_TitleText.text = name;
		((Text)_HyText).text = desc;
	}

	public void RefreshHeight()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if (_RT.sizeDelta.y != ((Text)_HyText).preferredHeight)
		{
			if (((Text)_HyText).preferredHeight < 40f)
			{
				if (_RT.sizeDelta.y != 40f)
				{
					_RT.sizeDelta = new Vector2(_RT.sizeDelta.x, 40f);
					((Graphic)_HyText).rectTransform.sizeDelta = new Vector2(((Graphic)_HyText).rectTransform.sizeDelta.x, 40f);
				}
			}
			else
			{
				_RT.sizeDelta = new Vector2(_RT.sizeDelta.x, ((Text)_HyText).preferredHeight);
				((Graphic)_HyText).rectTransform.sizeDelta = new Vector2(((Graphic)_HyText).rectTransform.sizeDelta.x, ((Text)_HyText).preferredHeight);
			}
		}
		Height = _RT.sizeDelta.y + 10f;
	}
}
