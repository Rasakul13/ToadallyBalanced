using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruitPrefabs = new GameObject[4];
    int rnd;

    bool positionIsFree;

    public void StartSpawn()
    {   
        SpawnFruit();
    }

    public void SpawnFruit()
    {
        rnd = Random.Range (0, 4);
        positionIsFree = false;    

        Vector2 spawnPosition = new Vector2(Random.Range(-7.3f, 7.3f), Random.Range(-3.6f, 3.2f));

        while(positionIsFree == false)
        {   

            //RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.up, 0f);

            RaycastHit2D hit = Physics2D.BoxCast(spawnPosition, new Vector2(1.5f,1.5f), 0f, Vector2.up, 0f);

            if(hit.collider != null)
            {
                Debug.Log("found object on spawnposition - recalculate position");
                spawnPosition = new Vector2(Random.Range(-6.7f, 7.3f), Random.Range(-3f, 3.65f));
            } 
            else 
            {
                //Debug.Log("spawnposition free");
                positionIsFree = true;
                
            }
        } 

        GameObject newFruit = Instantiate(fruitPrefabs[rnd]) as GameObject;
        newFruit.transform.position = spawnPosition; 
    }

    public void DespawnFruit()
    {
        Destroy(GameObject.FindWithTag("Fruit"));
    }
}
