using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int slimeCount = 1;
    public GameObject slime;
    private Vector2 _screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnCoroutine(3, slime, slimeCount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnCoroutine(int interval, GameObject enemy, int quantity)
    {
        yield return new WaitForSeconds(interval);
        for(int i = 0; i < quantity; i++)
        {
            GameObject newGameObject = Instantiate(enemy, new Vector3(Random.Range(_screenBounds.x * -1, _screenBounds.x * 1),
                                                Random.Range(_screenBounds.y * -1, _screenBounds.y * 1), 0), Quaternion.identity) as GameObject;
        }
        
            StartCoroutine(SpawnCoroutine(interval, enemy, quantity));
    }
}
