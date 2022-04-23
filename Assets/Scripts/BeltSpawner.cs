using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject astroidPrefab;
    public int astroidDensity;
    public int seed;
    public float innerRadius;
    public float outerRadius;
    public float height;
    public bool rotationClockwise;


    [Header("Astroid Settings")]
    public float minimumOrbitSpeed;
    public float maxOrbitSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;

    private Vector3 localPosition;
    private Vector3 worldOffset;
    private Vector3 worldPosition;
    private float randomRadius;
    private float randomRadian;
    private float x;
    private float y;
    private float z;

    private void Start()
    {
        Random.InitState(seed);
        for (int i= 0; i< astroidDensity; i++)
        {
            do
            {
                randomRadius = Random.Range(innerRadius, outerRadius);
                randomRadian = Random.Range(0, (2 * Mathf.PI));
                y = Random.Range(-(height / 2), (height / 2));
                x = randomRadius * Mathf.Cos(randomRadian);
                z = randomRadius * Mathf.Sin(randomRadian);
            }
            while (float.IsNaN(z)&& float.IsNaN(x));

            localPosition = new Vector3(x, y, z);

            worldOffset = transform.rotation * localPosition;
            worldPosition = transform.position + worldOffset;

            GameObject _asteroid = Instantiate(astroidPrefab, worldPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            _asteroid.AddComponent<BeltObject>().SetupBeltObject(Random.Range(minimumOrbitSpeed,maxOrbitSpeed),Random.Range(minRotationSpeed,maxRotationSpeed),gameObject,rotationClockwise) ;
            _asteroid.transform.SetParent(transform);
        }    
    }

}
