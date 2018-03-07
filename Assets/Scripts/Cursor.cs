using UnityEngine;

public class Cursor : MonoBehaviour {
	
	void Update () {
        transform.position = Input.mousePosition * Time.deltaTime;
	}
}
