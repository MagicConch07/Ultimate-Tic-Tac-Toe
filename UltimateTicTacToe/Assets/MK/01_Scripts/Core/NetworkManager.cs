using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Mk_JWT
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string _host;
        [SerializeField] private ushort _port;

        private void Update()
        {
            //* Test
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
    }

}
