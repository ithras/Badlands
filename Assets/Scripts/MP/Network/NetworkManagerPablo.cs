using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class NetworkManagerPablo : NetworkManager
{
    [SerializeField] private int minPlayers = 1;

    [Header("Scene")]
    [Scene] [SerializeField] private string menuScene = string.Empty;
    [Scene] [SerializeField] private string levelScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerPablo roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private NetworkGamePlayerPablo gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    public List<NetworkRoomPlayerPablo> RoomPlayers { get; } = new List<NetworkRoomPlayerPablo>();
    public List<NetworkGamePlayerPablo> GamePlayers { get; } = new List<NetworkGamePlayerPablo>();

    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    }

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach(var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        //if (SceneManager.GetActiveScene().name != menuScene)
        //{
        //    conn.Disconnect();
        //    return;
        //}
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        bool isLeader = RoomPlayers.Count == 0;

        NetworkRoomPlayerPablo roomPlayerInstance = Instantiate(roomPlayerPrefab);

        roomPlayerInstance.IsLeader = isLeader;

        NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerPablo>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach(var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers)
            return false;

        foreach(var player in RoomPlayers)
        {
            if (!player.IsReady)
                return false;
        }

        return true;
    }

    public void StartGame()
    {
        if(SceneManager.GetActiveScene().name == menuScene)
        {
            if (!IsReadyToStart())
                return;

            ServerChangeScene(levelScene);
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().name == menuScene && newSceneName.StartsWith("level"))
        {
            for(int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gamePlayerInstance = Instantiate(gamePlayerPrefab);
                gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                NetworkServer.Destroy(conn.identity.gameObject);

                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
            }
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("level"))
        {
            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }


}
