using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    void Start()
    {
        if (isLocalPlayer)
        {
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);
    }
}
