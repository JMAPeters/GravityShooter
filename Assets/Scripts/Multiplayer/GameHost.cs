using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameHost : NetworkBehaviour {

    private NetworkManager networkManager;

    uint roomSize = 0;
    int gameMode = 0;
    private string roomName;
    public InputField roomNameInput;

    public Button CreateRoomButton;
    public Toggle Toggle1v1;
    public Toggle Toggle2v2;
    public Toggle ToggleTD;
    public Toggle ToggleFFA;


    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(Text name)
    {
        roomName = name.text;
    }

    void Update()
    {
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        if (Toggle1v1.isOn)
        {
            roomSize = 2;
            ToggleTD.interactable = false;
            ToggleFFA.interactable = true;
        }

        if (Toggle2v2.isOn)
        {
            roomSize = 4;
            ToggleTD.interactable = true;
            ToggleFFA.interactable = false;
        }

        if ((!Toggle1v1.isOn && !Toggle2v2.isOn) || Toggle1v1.isOn && Toggle2v2.isOn)
        {
            roomSize = 0;
            ToggleTD.interactable = false;
            ToggleFFA.interactable = false;
        }

        if (ToggleTD.isOn)
            gameMode = 1; // TeamDeathmatch

        if (ToggleFFA.isOn)
            gameMode = 2; //FreeForAll

        if (!ToggleTD.isOn && !ToggleFFA.isOn)
            gameMode = 0; //nonGamemode

        if (roomSize != 0 && gameMode != 0)
            CreateRoomButton.interactable = true;
        else
            CreateRoomButton.interactable = false;
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            //create a room
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", gameMode, 0, networkManager.OnMatchCreate);
            
        }
    }
}
