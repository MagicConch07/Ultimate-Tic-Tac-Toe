using UnityEngine;

namespace Mk_JWT
{
    public enum SendType
    {
        GET,
        POST
    }
    public class NetworkEvent
    {
        public SendType sendType;
    }

    public static class Events
    {
        public static RegisterNetworkEvnet RegisterNetworkEvnet = new RegisterNetworkEvnet();
        public static PostResultEvent PostResultEvent = new PostResultEvent();
        public static LoginNetworkEvent LoginNetworkEvent = new LoginNetworkEvent();
    }

    public class RegisterNetworkEvnet : NetworkEvent, IWWWFormable
    {
        public string email;
        public string name;
        public string password;

        public WWWForm GetWWWForm()
        {
            var form = new WWWForm();
            form.AddField("email", email);
            form.AddField("name", name);
            form.AddField("password", password);

            return form;
        }
    }

    public class LoginNetworkEvent : NetworkEvent, IWWWFormable
    {
        public string email;
        public string password;

        public WWWForm GetWWWForm()
        {
            var form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);

            return form;
        }
    }

    public class PostResultEvent : NetworkEvent
    {
        public string uri;
        public ResponseMsg response;
    }
}
