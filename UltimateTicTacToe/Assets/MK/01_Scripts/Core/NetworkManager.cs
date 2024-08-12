using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Mk_JWT
{
    public enum MsgType : int
    {
        ERROR = 0,
        SUCCESS = 1,
    }

    [System.Serializable]
    public class ResponseMsg
    {
        public MsgType type;
        public string msg;
    }

    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string _host;
        [SerializeField] private ushort _port;

        public NetworkEventChannelSO registerChannel, postResultChannel, loginChannel;
        public SetDataEventCahnnelSO tokenChannel;

        [field: SerializeField, TextArea] public string Token { get; private set; } = string.Empty;

        private void Awake()
        {
            registerChannel.OnRaiseEvent.AddListener(HandleRegisterEvent);
            loginChannel.OnRaiseEvent.AddListener(HandleLoginEvent);
            tokenChannel.OnRaiseEvent.AddListener(HandleTokenChangeEvent);
        }

        private void Start()
        {
            Token = PlayerPrefs.GetString("Token", "");

            if (Token != "")
            {
                GetRequest("verify_token", (res) =>
                {
                    if (res.type == MsgType.SUCCESS)
                    {
                        //TODO 여기 나중에 게임 처리 어케할지 결정하고 처리 지금은 로그인만
                        //gameStartChannel.RaiseEvent(DataEvents.VoidDataEvent);
                        Debug.Log("이미 로그인된 유저");
                    }
                });
            }
        }

        [ContextMenu("DeleteToken")]
        private void DeleteToken()
        {
            PlayerPrefs.DeleteKey("Token");
        }


        private void SetRequestToken(UnityWebRequest req)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                req.SetRequestHeader("Authorization", $"Bearer{Token}");
            }
        }

        private void HandleTokenChangeEvent(DataEvent evt)
        {
            var strEvt = evt as StringDataEvent;
            Token = strEvt.data;
            PlayerPrefs.SetString("Token", Token);
        }

        private void HandleLoginEvent(NetworkEvent evt)
        {
            var loginEvt = evt as LoginNetworkEvent;
            PostRequest("user_login", loginEvt.GetWWWForm());
        }

        private void HandleRegisterEvent(NetworkEvent evt)
        {
            var registerEvt = evt as RegisterNetworkEvent;
            PostRequest("user_register", registerEvt.GetWWWForm());
        }

        public void GetRequest(string path, Action<ResponseMsg> Callback = null)
        {
            StartCoroutine(GetCoroutine(path, Callback));
        }

        private IEnumerator GetCoroutine(string path, Action<ResponseMsg> Callback)
        {
            string url = $"{_host}:{_port}/{path}";

            using (var req = UnityWebRequest.Get(url))
            {
                SetRequestToken(req); //토큰 셋팅

                yield return req.SendWebRequest();

                //if(req.responseCode == UnityWebRequest.Result.Success)

                Debug.Log(req.downloadHandler.text);
                ResponseMsg responseMsg = JsonUtility.FromJson<ResponseMsg>(req.downloadHandler.text);
                Callback?.Invoke(responseMsg);
            }
        }

        public void PostRequest(string path, WWWForm form)
        {
            StartCoroutine(PostCoroutine(path, form));
        }

        private IEnumerator PostCoroutine(string path, WWWForm form)
        {
            string url = $"{_host}:{_port}/{path}";

            using (var req = UnityWebRequest.Post(url, form))
            {
                yield return req.SendWebRequest();

                ResponseMsg msg = JsonUtility.FromJson<ResponseMsg>(req.downloadHandler.text);

                var evt = Events.PostResultEvent;
                evt.uri = path;
                evt.response = msg;

                postResultChannel.RaiseEvent(evt);
            }
        }
    }
}
