using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine;

public class PreGame : NetworkBehaviour {

    private NetworkManager networkManager;
    private MatchInfoSnapshot currentMatch;

    public Text roomName;
    public Text roomSize;
    public Toggle ToggleSpion;
    public Toggle ToggleSoldier;
    public Toggle ToggleTank;
    public string playerName;
    public Image playerPreview;
    public Toggle isReady;

    string playerCaracter;

	void Start () {
        networkManager = NetworkManager.singleton;

        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        Debug.Log("jkfdls;ajlads");
        Debug.Log(currentMatch.name);

        //roomName.text = currentMatch.name;
        //roomSize.text = "(" + currentMatch.currentSize + "/" + currentMatch.maxSize + ")";
    }

    public void OnMatchList(bool succes, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (!succes || matchList == null)
        {
            return;
        }
        else
        {
            foreach (MatchInfoSnapshot match in matchList)
            {
                if (networkManager.matchInfo.networkId == match.networkId)
                {
                    currentMatch = match;
                    Debug.Log(match);
                    return;
                }
            }
        }
    }

        // Update is called once per frame
        void Update () {
        ToggleCaracter();
	}

    void ToggleCaracter()
    {
        if (ToggleSpion.isOn)
        {
            //  
        }


        if (playerName != "" && playerName != null && playerCaracter != null)
            isReady.interactable = true;
        else
            isReady.interactable = false;
    }

    public void SetRoomName(Text name)
    {
        playerName = name.text;
    }
}
