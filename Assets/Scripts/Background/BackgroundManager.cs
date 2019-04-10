using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject backgroundCirclePrefab;

	void Start ()
    {
        Invoke("SpawnCircle", 0f);
	}
	
	void SpawnCircle ()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundCircle").Length < 10)
        {
            int width = Screen.width;
            int height = Screen.height;
            Instantiate(backgroundCirclePrefab, new Vector3(Random.Range(-6, 6), Random.Range(-6, 6)), Quaternion.identity, gameObject.transform);
        }
        Invoke("SpawnCircle", Random.Range(0f, 2f));
    }
}
