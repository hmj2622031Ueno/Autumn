using UnityEngine;

public class ItemMove : MonoBehaviour
{
	[SerializeField] float scrollSpeed = 6.0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

		// âÊñ äOÇ‹Ç≈çsÇ¡ÇΩÇÁè¡Ç∑
		if(transform.position.x < -15f)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
