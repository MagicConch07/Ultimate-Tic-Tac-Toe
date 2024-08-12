using Mk_JWT;
using UnityEngine;

public class Test : MonoBehaviour
{
    public NetworkEventChannelSO registerChannel, postResultChannel, loginChannel;
    public SetDataEventCahnnelSO tokenChannel;

    private readonly string _path = "user_register";

    public string Email
    {
        get;
        private set;
    }
    public string Password
    {
        get;
        private set;
    }
    public string Name
    {
        get;
        private set;
    }

    private void Awake()
    {
        postResultChannel.OnRaiseEvent.AddListener(HandlePostEvent);
    }

    private void HandlePostEvent(NetworkEvent evt)
    {
        var postEvt = evt as PostResultEvent;
        if (postEvt.uri != _path) return;

        if (postEvt.response.type == MsgType.SUCCESS)
        {
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
            Debug.Log("성공");
        }
        Debug.Log(postEvt.response.msg); //이부분도 팝업창을통해 메시지를 출력해야하지만..안할꺼야.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RegisterWork();
        }
    }

    private void RegisterWork()
    {
        var evt = Events.RegisterNetworkEvent;
        evt.email = "test@gmail.com";
        evt.password = "1234";
        evt.name = "TestAdmin";

        registerChannel.RaiseEvent(evt);
    }
}
