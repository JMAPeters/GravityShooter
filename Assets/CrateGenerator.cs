using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateGenerator : MonoBehaviour {

    public List<GameObject> Crates;
    public int maxCrateCount, minCrateCount;
    public GameObject Crate;
    private int xPos, yPos, random;
    private float spawnDelay, spawnTimer;

	// Use this for initialization
	void Start () {
        Crates = new List<GameObject>();
        spawnDelay = 2.0f;
        spawnTimer = spawnDelay;
        addCrate();
    }


    public void addCrate()
    {
        Rigidbody2D rbCrate = Crate.GetComponent<Rigidbody2D>();
        Crate.transform.position = RandomPosition();
        Crates.Add(Crate);
        Instantiate(Crate);
    }

    private Vector2 RandomPosition()
    {
        random = Random.Range(0, 3);
        switch (random)
        {          
            case 0:
                xPos = 15; 
                yPos = Random.Range(-7, 0);
                break;
            case 1:
                xPos = Random.Range(-15, 4);
                yPos = -7;
            break;
            case 2:
                xPos = -15;
                yPos = Random.Range(0, 7);
                break;
            case 3:
                xPos = Random.Range(-4, 15);
                yPos = 7;
            break;
        }

        Vector2 Position = new Vector2(xPos, yPos);
        return Position;
        //TODO: Have the position be based on the position of the 2 base planets.
    }

	// Update is called once per frame
	void Update () {
		if(Crates.Count < minCrateCount || (spawnTimer <= 0 && Crates.Count < maxCrateCount))
        {
            addCrate();
            Debug.Log("Dit werkt");
            spawnTimer = spawnDelay;
            foreach (GameObject c in Crates)
            {
                Rigidbody2D rbc = c.GetComponent<Rigidbody2D>();
                if (c.transform.position.x < )
            }
        }
        spawnTimer = spawnTimer - 0.01f;
	}
}
