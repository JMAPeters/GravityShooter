using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    private NetworkManager networkManager;

    int gameMode = 0;

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

        RefreshRoomList();
    }

    void Update()
    {
        ToggleMenu();
    }

    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    public void OnMatchList(bool succes, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {

        if (!succes || matchList == null)
        {
            return;
        }
        
        foreach (MatchInfoSnapshot match in matchList)
        {
            if (match.averageEloScore == gameMode)
            {
                GameObject roomListItemGameObject = Instantiate(roomListItemPrefab);
                roomListItemGameObject.transform.SetParent(roomListParent);

                RoomListItem roomListItem = roomListItemGameObject.GetComponent<RoomListItem>();
                if (roomListItem != null)
                {
                    roomListItem.Setup(match, JoinRoom);
                }

                roomList.Add(roomListItemGameObject);
            }
        }
    }

    private void ToggleMenu()
    {
        if (Toggle1v1.isOn)
        {
            ToggleTD.interactable = false;
            ToggleFFA.interactable = true;
        }

        if (Toggle2v2.isOn)
        {
            ToggleTD.interactable = true;
            ToggleFFA.interactable = false;
        }

        if ((!Toggle1v1.isOn && !Toggle2v2.isOn) || Toggle1v1.isOn && Toggle2v2.isOn)
        {
            ToggleTD.interactable = false;
            ToggleFFA.interactable = false;
        }

        if (ToggleTD.isOn)
        { 
            gameMode = 1; // TeamDeathmatch
        }

        if (ToggleFFA.isOn)
        {
            gameMode = 2; //FreeForAll
        }

        if (!ToggleTD.isOn && !ToggleFFA.isOn)
        {
            gameMode = 0; //nonGamemode
        }
    }

        void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot matchinfo)
    {
        networkManager.matchMaker.JoinMatch(matchinfo.networkId, "", "", "", gameMode, 0, networkManager.OnMatchJoined);
        ClearRoomList();
    }
}
