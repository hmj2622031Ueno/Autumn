using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField] Sprite[] walkSprites;
	[SerializeField] Sprite jumpSprite;
	float time = 0;
	int idx = 0;
	int jumpCount = 0;
	Collider2D playerCollider;
	SpriteRenderer spriteRenderer;
	bool dropDown = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerCollider = GetComponent<Collider2D>();
    }

	// Update is called once per frame
	void Update()
	{
		// ジャンプ
		if (Keyboard.current.upArrowKey.wasPressedThisFrame && jumpCount < 2)
		{
			rigid2D.linearVelocity = new Vector2(rigid2D.linearVelocity.x, 12f);
			jumpCount++;
			if(rigid2D.linearVelocity.y != 0)
			{
				spriteRenderer.sprite = jumpSprite;
			}
		}

		// 右キーアニメーション
		if(Keyboard.current.rightArrowKey.isPressed)
		{
			// アニメーション
			time += Time.deltaTime;
			if (time > 0.1f)
			{
				time = 0;
				spriteRenderer.sprite = walkSprites[idx];
				idx++;
				if (idx >= 3) { idx = 0; }
			}
		}

		// 下キー入力で下段へ
		GameObject ground = GameObject.FindGameObjectWithTag("Ground");
		if (Keyboard.current.downArrowKey.wasPressedThisFrame)
		{
			if(ground != null)
			{
				dropDown = true;
				Collider2D groundCollider = ground.GetComponent<Collider2D>();
				Physics2D.IgnoreCollision(playerCollider, groundCollider, false);
			}
		}

		// 下段から上段へ
		if(ground != null)
		{
			Collider2D groundCollider = ground.GetComponent<Collider2D>();
			float groundY = groundCollider.bounds.max.y;
			if(transform.position.y <  groundY)
			{
				Physics2D.IgnoreCollision(playerCollider, groundCollider, true);
			}
			else if(!dropDown)
			{
				Physics2D.IgnoreCollision(playerCollider, groundCollider, false);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		jumpCount = 0;
	}
}
