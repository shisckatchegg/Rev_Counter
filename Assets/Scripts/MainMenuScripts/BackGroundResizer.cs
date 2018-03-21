using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BackGroundResizer : MonoBehaviour
{
	public bool KeepAspectRatio = true;

	public bool ExecuteOnUpdate = true;

	void Start()
	{
		Resize(KeepAspectRatio);
	}

	void FixedUpdate()
	{
		if (ExecuteOnUpdate)
		{
			Resize(KeepAspectRatio);
		}
	}

	void Resize(bool keepAspect = false)
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		transform.localScale = new Vector3(1, 1, 1);

		float width = spriteRenderer.sprite.bounds.size.x;
		float height = spriteRenderer.sprite.bounds.size.y;

		float worldScreenHeight = Camera.main.orthographicSize * 2f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		Vector3 imgScale = new Vector3(1f, 1f, 1f);

		if (keepAspect)
		{
			Vector2 ratio = new Vector2(width / height, height / width);
			if ((worldScreenWidth / width) > (worldScreenHeight / height))
			{
				// wider than taller
				imgScale.y = worldScreenHeight / height;
				imgScale.x = imgScale.y * ratio.x;
			}
			else
			{
				// taller than wider
				imgScale.x = worldScreenWidth / width;
				imgScale.y = imgScale.x * ratio.y;
			}
		}
		else
		{
			imgScale.x = worldScreenWidth / width;
			imgScale.y = worldScreenHeight / height;
		}

		transform.localScale = imgScale;
	}
}