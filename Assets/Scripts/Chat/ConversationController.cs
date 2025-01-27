using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    public Transform npcTransform;
    public float range = 1f;
    public Button chatButton;
    public Button exitButton;
    public ThirdPersonController thirdPersonController;
    public StarterAssetsInputs inputs;

    private void Start()
    {
        chatButton.onClick.AddListener(OnChatButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }


    private void OnDestroy()
    {
        chatButton.onClick.RemoveListener(OnChatButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C)) chatButton.onClick.Invoke();
        if (GetDistance() < range) chatButton.gameObject.SetActive(true);
        else chatButton.gameObject.SetActive(false);
    }

    private void OnExitButtonClicked()
    {
        thirdPersonController.LockCameraPosition = false;
        inputs.enableMovement = true;
    }

    private void OnChatButtonClicked()
    {
        thirdPersonController.LockCameraPosition = true;
        inputs.enableMovement = false;
    }

    private float GetDistance()
    {
        return Vector3.Distance(transform.position, npcTransform.position);
    }

}
