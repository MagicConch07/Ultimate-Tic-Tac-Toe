using UnityEngine;
using Mk_JWT;
using UnityEngine.UIElements;
using System;

public class RegisterScreen : IDisposable
{
    private readonly string path = "user_register";

    private MembershipScreen _main;
    private VisualElement _root;
    private TextField _emailField, _passwordField, _nameField;

    public string Email
    {
        get => _emailField.value;
        set => _emailField.value = value;
    }

    public string Password
    {
        get => _passwordField.value;
        set => _passwordField.value = value;
    }

    public string Name
    {
        get => _nameField.value;
        set => _nameField.value = value;
    }

    public RegisterScreen(VisualElement root, MembershipScreen main)
    {
        _root = root;
        _main = main;

        _emailField = root.Q<TextField>("EmailField");
        _passwordField = root.Q<TextField>("PasswordField");
        _nameField = root.Q<TextField>("NameField");

        main.postResultChannel.OnRaiseEvent.AddListener(HandleResponse);
    }

    public void Dispose()
    {
        _main.postResultChannel.OnRaiseEvent.RemoveListener(HandleResponse);
    }

    private void HandleResponse(NetworkEvent evt)
    {
        var postEvt = evt as PostResultEvent;

        if (postEvt.uri != path) return;

        if (postEvt.response.type == MsgType.ERROR)
        {
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        // TODO : 팝업창을 통해 메세지 출력
        Debug.Log(postEvt.response.msg);
    }
}
