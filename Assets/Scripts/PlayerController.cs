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
	[SerializeField] Collider2D upperGroundCollider1;
	[SerializeField] Collider2D upperGroundCollider2;
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

		// アニメーション
		if(jumpCount == 0)
		{
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
		if (Keyboard.current.downArrowKey.wasPressedThisFrame)
		{
			dropDown = true;
		}

		// 上段の当たり判定
		CheckGround(upperGroundCollider1);
		CheckGround(upperGroundCollider2);
		if(dropDown)
		{
			bool belowGround1 = playerCollider.bounds.max.y < upperGroundCollider1.bounds.min.y;
			bool belowGround2 = playerCollider.bounds.max.y < upperGroundCollider2.bounds.min.y;
			if(belowGround1 && belowGround2)
			{
				dropDown = false;
			}
		}
	}

	void CheckGround(Collider2D groundCollider)
	{
		if (groundCollider == null)
		{
			return;
		}

		// 下段から上段はすり抜け
		if (rigid2D.linearVelocity.y > 0)
		{
			Physics2D.IgnoreCollision(playerCollider, groundCollider, true);
		}
		// 下キー押しですり抜け
		else if(dropDown)
		{
			Physics2D.IgnoreCollision(playerCollider, groundCollider, true);
		}

		else
		{
			Physics2D.IgnoreCollision(playerCollider, groundCollider, false);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		jumpCount = 0;
		spriteRenderer.sprite = walkSprites[0];
	}
}
