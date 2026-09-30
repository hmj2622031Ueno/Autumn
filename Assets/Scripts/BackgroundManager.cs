using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField] float scrollSpeed = 6.0f;
	[SerializeField] float backgroundWidth = 18.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Keyboard.current.rightArrowKey.isPressed)
		{
			transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
			if(transform.GetChild(0).position.x <= -backgroundWidth)
			{
				transform.GetChild(0).position += Vector3.right * backgroundWidth * 2f;
			}
			if(transform.GetChild(1).position.x <= -backgroundWidth)
			{
				transform.GetChild(1).position += Vector3.right * backgroundWidth * 2f;
			}
		}

	}
}
