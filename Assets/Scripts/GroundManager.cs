using UnityEngine;

public class GroundManager : MonoBehaviour
{
	[SerializeField] float scrollSpeed = 6.0f;
	[SerializeField] float groundWidth = 18.0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
		if (transform.GetChild(0).position.x <= -groundWidth)
		{
			transform.GetChild(0).position += Vector3.right * groundWidth * 2f;
		}
		if (transform.GetChild(1).position.x <= -groundWidth)
		{
			transform.GetChild(1).position += Vector3.right * groundWidth * 2f;
		}
	}
}
