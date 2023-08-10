using UnityEngine;

public class UI2DSpriteAnimation : MonoBehaviour
{
	public int framerate = 20;

	public bool ignoreTimeScale = true;

	public Sprite[] frames;

	private SpriteRenderer mUnitySprite;

	private UI2DSprite mNguiSprite;

	private int mIndex;

	private float mUpdate;

	private void Start()
	{
		mUnitySprite = ((Component)this).GetComponent<SpriteRenderer>();
		mNguiSprite = ((Component)this).GetComponent<UI2DSprite>();
		if (framerate > 0)
		{
			mUpdate = (ignoreTimeScale ? RealTime.time : Time.time) + 1f / (float)framerate;
		}
	}

	private void Update()
	{
		if (framerate == 0 || frames == null || frames.Length == 0)
		{
			return;
		}
		float num = (ignoreTimeScale ? RealTime.time : Time.time);
		if (mUpdate < num)
		{
			mUpdate = num;
			mIndex = NGUIMath.RepeatIndex((framerate > 0) ? (mIndex + 1) : (mIndex - 1), frames.Length);
			mUpdate = num + Mathf.Abs(1f / (float)framerate);
			if ((Object)(object)mUnitySprite != (Object)null)
			{
				mUnitySprite.sprite = frames[mIndex];
			}
			else if ((Object)(object)mNguiSprite != (Object)null)
			{
				mNguiSprite.nextSprite = frames[mIndex];
			}
		}
	}
}
