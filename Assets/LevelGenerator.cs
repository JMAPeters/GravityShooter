using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject planetPref;
    public GameObject planet1;
    public GameObject planet2;   
    public List<GameObject> planets;
    Vector2 posPlanet1;
    Vector2 posPlanet2;
    float randomFactor;
    public int PlanetDistance = 5;
    int isDone = 0;
    public int planetCount = 0;
    bool planeet1 = false;


    // Use this for initialization
    void Start ()
    {   
        planets = new List<GameObject>();
 
        planets.Add(planet2);
        planets.Add(planet1);

        newPlanetPos();
    }

    public void newPlanetPos()
    {
        if (planets.Count == planetCount)
        { 
            return;
        }

        if (isDone == 600)
            return;
       

        GameObject lastPlanet = lastPlanet = planets[planets.Count - 1]; ;

        if (isDone >= 200)
        {
            lastPlanet = planets[planets.IndexOf(lastPlanet)  - 1];           
        }
        if (isDone >= 400)
        {
            lastPlanet = planets[planets.IndexOf(lastPlanet) - 1];          
        }


        SpriteRenderer spriteLastPlanet = lastPlanet.GetComponent<SpriteRenderer>();
       // int randomPlanetNumber = Random.Range(0, planets.Count - 1);

       // GameObject randomPlanet = planets[randomPlanetNumber];
        //SpriteRenderer spriteRandomPlanet = randomPlanet.GetComponent<SpriteRenderer>();

        randomFactor = Random.Range(10, 21) / 10f;
        if(randomFactor <= 1.5f)
        {
            randomFactor = Random.Range(10, 21) / 10f;
        }
        addPlanet(randomFactor);

        GameObject newPlanet = planets[planets.Count - 1];
        SpriteRenderer spriteNewPlanet = newPlanet.GetComponent<SpriteRenderer>();

        Vector3 randomDir = new Vector2(Random.Range(-10, 11) / 10f, Random.Range(-10, 11) / 10f);

        float distance = spriteLastPlanet.bounds.extents.x + PlanetDistance * randomFactor + spriteNewPlanet.bounds.extents.x;

        newPlanet.transform.position = lastPlanet.transform.position + randomDir.normalized * distance;
        //newPlanet.transform.position = new Vector3(Random.Range(planet1.transform.position.x, planet2.transform.position.x + 1), Random.Range(planet1.transform.position.y, planet2.transform.position.y + 1));
            if (newPlanet.transform.position.x >= planet1.transform.position.x && newPlanet.transform.position.x <= planet2.transform.position.x &&
                newPlanet.transform.position.y <= planet2.transform.position.y && newPlanet.transform.position.y >= planet1.transform.position.y)
            {
                bool isColliding = Physics2D.OverlapCircle(newPlanet.transform.position, PlanetDistance * randomFactor + spriteNewPlanet.bounds.extents.x - 1);
                //bool isNotIsolated = Physics2D.OverlapCircle(newPlanet.transform.position, PlanetDistance * randomFactor + spriteNewPlanet.bounds.extents.x);
                if (!isColliding)
                {
                    Instantiate(newPlanet);
                    Debug.Log("Planeet is goed geplaatst");              
                    isDone = 0;                                 
                    newPlanetPos();
                }
                else
                {
                    planets.Remove(newPlanet);
                    isDone++;
                    newPlanetPos();
                }
            }
            else
            {
                planets.Remove(newPlanet);
                isDone++;
                Debug.Log("eerste if");
            Debug.Log(newPlanet.transform.position);
                newPlanetPos();
               
            }
        
    }

    public void addPlanet(float random)
    {
        GameObject planet = planetPref;
        Rigidbody2D rbPlanet = planet.GetComponent<Rigidbody2D>();
        planet.transform.localScale = new Vector2(random * 10, random * 10);
        rbPlanet.mass = 30 * random;

        planets.Add(planet);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    
}
