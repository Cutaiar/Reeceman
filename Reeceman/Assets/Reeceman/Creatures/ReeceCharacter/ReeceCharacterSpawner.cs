using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeceCharacterSpawner : MonoBehaviour
{

    public float spawnInterval;
    public Transform spawnPoint;
    public ReeceHumanFormController reeceCharacterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnCharacter", spawnInterval, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnCharacter()
    {
        Instantiate(reeceCharacterPrefab, spawnPoint.position, spawnPoint.rotation, null);
    }
}
