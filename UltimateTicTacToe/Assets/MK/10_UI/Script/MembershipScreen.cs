using Mk_JWT;
using UnityEngine;
using UnityEngine.UIElements;

public class MembershipScreen : MonoBehaviour
{
    public VisualTreeAsset registerTree;

    private UIDocument _uiDocument;
    private VisualElement _root;

    public NetworkEventChannelSO registerChannel, postResultChannel, loginChannel;

    private void Awake()
    {

    }

    private void HandleRegisterBtnClick()
    {
        var template = registerTree.Instantiate();

        new RegisterScreen(template, this);
    }
}
