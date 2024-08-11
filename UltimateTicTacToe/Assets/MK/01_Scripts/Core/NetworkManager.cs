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

        private void Awake()
        {
            registerChannel.OnRaiseEvent.AddListener(HandleRegisterEvent);
            loginChannel.OnRaiseEvent.AddListener(HandleLogineEvnet);
        }

        private void HandleLogineEvnet(NetworkEvent evt)
        {
            var registerEvt = evt as LoginNetworkEvent;
            PostRequest("users_login", registerEvt.GetWWWForm());
        }

        private void HandleRegisterEvent(NetworkEvent evt)
        {
            var registerEvt = evt as RegisterNetworkEvnet;
            PostRequest("users_register", registerEvt.GetWWWForm());
        }

        private void Update()
        {
            //! Test
            if (Input.GetKeyDown(KeyCode.P))
            {
                GetRequest();
            }
        }

        public void GetRequest()
        {
            StartCoroutine(GetCoroutine("test"));
        }

        private IEnumerator GetCoroutine(string path)
        {
            string url = $"{_host}:{_port}/{path}";

            using (var req = UnityWebRequest.Get(url))
            {
                yield return req.SendWebRequest();

                Debug.Log(req.downloadHandler.text);
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
