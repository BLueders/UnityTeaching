using UnityEngine;
using System.Collections;

/// <summary>
/// The BrainSpawn is what handles the spawning of Brains on random positions on the playing field.
/// There can only be a limited amount of brains on the field, when there are too many, no new brains will spawn.
/// </summary>
public class BrainSpawn : MonoBehaviour {

	// the prefab to clone brains from
    public GameObject brainPrefab;

    int numberOfActiveBrains = 0;
    int maxNumberOfBrains = 5;

    // Start spawn coroutine in start function, alternatively have a timer here that substract Time.deltaTime each frame
    void Start(){
        StartCoroutine(SpawnBrainsCoroutine());
    }

    IEnumerator SpawnBrainsCoroutine(){
        // forever
        while(true){
            if(numberOfActiveBrains < maxNumberOfBrains){
                // get a random Vector up to 5 Units away from the center
                Vector2 randomPos = Random.insideUnitCircle * 5f;
                // instantiate brain prefab at random position (the function takes a vector3, so we need to create one)
                Instantiate(brainPrefab, new Vector3(randomPos.x, randomPos.y, 0), Quaternion.identity);
                numberOfActiveBrains++;
            }
            // wait for x seconds until next spawn
            yield return new WaitForSeconds(Random.Range(2,5));
        }
    }

    // option for other instances to tell the brain spawn that a brain was collected
    public void OnCollectBrain(){
        numberOfActiveBrains--;
    }
}
