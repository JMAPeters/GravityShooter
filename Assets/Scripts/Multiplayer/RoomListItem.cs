using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot matchinfo);
    private JoinRoomDelegate joinRoomDelegate;

    private MatchInfoSnapshot match;

    [SerializeField]
    private Text roomNameText;

    public void Setup(MatchInfoSnapshot matchinfo, JoinRoomDelegate joinRoomCallback)
    {
        match = matchinfo;

        joinRoomDelegate = joinRoomCallback;

        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinRoomDelegate.Invoke(match);
    }
}
