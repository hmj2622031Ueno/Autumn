using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] GameObject acorn1Prefab;
	[SerializeField] GameObject acorn2Prefab;
	[SerializeField] GameObject acorn3Prefab;
	[SerializeField] GameObject fallenLeavesPrefab;

	[SerializeField] Transform upperSpawnPoint1;
	[SerializeField] Transform upperSpawnPoint2;
	[SerializeField] Transform lowerSpawnPoint;

	[SerializeField] float spawnInterval = 2f;
	[SerializeField] float spawnChance = 0.7f;

	float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if(timer >= spawnInterval)
		{
			timer = 0;
			SpawnItem(upperSpawnPoint1);
			SpawnItem(upperSpawnPoint2);
			SpawnItem(lowerSpawnPoint);
		}
    }

	void SpawnItem(Transform spawnPoint)
	{
		if(spawnPoint == null)
		{
			return;
		}
		if(Random.value > spawnChance)
		{
			return;
		}
		GameObject prefab;

		if(Random.value < 0.2f)
		{
			prefab = fallenLeavesPrefab;
		}
		else
		{
			int randomAcorn = Random.Range(0, 3);
			if(randomAcorn == 0)
			{
				prefab = acorn1Prefab;
			}
			else if(randomAcorn == 1)
			{
				prefab = acorn2Prefab;
			}
			else
			{
				prefab = acorn3Prefab;
			}
		}
		
		GameObject item = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		Collider2D itemCollider = item.GetComponent<Collider2D>();

		if(itemCollider != null)
		{
			float bottomOffset = item.transform.position.y - itemCollider.bounds.min.y;

			item.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y +  bottomOffset, item.transform.position.z);
		}
	}
}
