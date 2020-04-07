using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChatWindowUI : NetworkBehaviour
{
	[SerializeField] private GameObject panel = null;
	[SerializeField] private TMP_InputField inputField = null;
	[SerializeField] private TMP_Text chatHistory = null;
	[SerializeField] private Scrollbar scrollBar = null;

	public static event Action<uint, string, string> OnMessage;

	private bool isEnable = false;

	private NetworkManagerPablo manager;
	private NetworkManagerPablo Manager
	{
		get
		{
			if (manager != null)
				return manager;

			return manager = NetworkManager.singleton as NetworkManagerPablo;
		}
	}

	public void chatHandler()
	{
		isEnable = !isEnable;
		panel.SetActive(isEnable);
		scrollBar.gameObject.SetActive(isEnable);

		if (isEnable)
		{
			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
			inputField.OnPointerClick(new PointerEventData(EventSystem.current));
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Send(inputField.text);
		}
	}

	public override void OnStartAuthority()
	{
		OnMessage += HandleNewMessage;
	}

	[ClientCallback]
	private void OnDestroy()
	{
		if (!hasAuthority)
			return;

		OnMessage -= HandleNewMessage;
	}

	[Client]
	public void Send(string message)
	{
		if (string.IsNullOrWhiteSpace(message))
			return;

		CmdSendMessage(message);

		inputField.text = string.Empty;
	}

	[Command]
	private void CmdSendMessage(string message)
	{
		string playerName = Manager.GamePlayers.Find(x => x.connectionToClient == connectionToClient).DisplayName;
		RpcHandleMessage(netId, playerName, message);
	}

	[ClientRpc]
	private void RpcHandleMessage(uint playerNetId,string playerName, string message)
	{
		OnMessage?.Invoke(playerNetId, playerName, message);
	}

	private void HandleNewMessage(uint playerNetId, string playerName, string message)
	{

		string prettyMessage = playerNetId == netId ?
			$"\n<color=yellow>[{playerName}]</color>: {message}" :
			$"\n<color=blue>[{playerName}]</color>: {message}";

		chatHistory.text += prettyMessage;
		scrollBar.value = 0;
	}
}

