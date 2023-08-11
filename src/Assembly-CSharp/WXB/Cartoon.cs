using System;
using UnityEngine;

namespace WXB;

[Serializable]
public class Cartoon
{
	public string name;

	public float fps;

	public Sprite[] sprites;

	public float space = 2f;

	public int width;

	public int height;
}
