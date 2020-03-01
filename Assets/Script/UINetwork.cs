using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class UINetwork : MonoBehaviour
{
    GameObject panelMultiplayer;

    Button bttnHost;
    Button bttnJoin;
    Button bttnCancel;
    Text txInfo;

    NetworkManager network;

    int status = 0;
    void Start()
    {
        panelMultiplayer = GameObject.Find("PanelMultiplayer");
        panelMultiplayer.transform.localPosition = Vector3.zero;

        bttnHost = GameObject.Find("ButtonHost").GetComponent<Button>();
        bttnJoin = GameObject.Find("ButtonJoin").GetComponent<Button>();
        bttnCancel = GameObject.Find("ButtonCancel").GetComponent<Button>();
        txInfo = GameObject.Find("infoServer").GetComponent<Text>();
        bttnHost.onClick.AddListener(StartHostGamePlay);
        bttnJoin.onClick.AddListener(StartJoinGamePlay);
        bttnCancel.onClick.AddListener(CancelConnection);
        bttnCancel.interactable = false;

        network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.active || NetworkServer.active)
        {
            bttnHost.interactable = false;
            bttnJoin.interactable = false;
            bttnCancel.interactable = true;
        }
        else
        {
            bttnHost.interactable = true;
            bttnJoin.interactable = true;
            bttnCancel.interactable = false;
        }

        if (NetworkServer.connections.Count == 2 && status == 0)
        {
            status = 1;
            startGamePlay();
        }
        if (ClientScene.ready && !NetworkServer.active && status == 0)
        {
            status = 1;
            startGamePlay();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            backToMenu();
        }
    }

    private void StartHostGamePlay()
    {
        Debug.Log("Host Button Is Being Pressed");
        if (!NetworkServer.active)
        {
            network.StartHost();
        }
        if (NetworkServer.active) txInfo.text = "Info: Waiting for another Player ";
    }
    private void StartJoinGamePlay()
    {
        Debug.Log("Join Button Is Being Pressed");
        if (!NetworkClient.active)
        {
            network.StartClient();
            network.client.RegisterHandler(MsgType.Disconnect, ConnectionError);
        }
        if (NetworkClient.active) txInfo.text = "Info: Try connecting to the server";
    }
    private void CancelConnection()
    {
        Debug.Log("Cancel Button Is Being Pressed");
        network.StopHost();
        bttnHost.interactable = true;
        bttnJoin.interactable = true;
        bttnCancel.interactable = false;
        txInfo.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
    }
    private void ConnectionError(NetworkMessage netMsg)
    {
        network.StopClient();
        txInfo.text = "Info: Connection is lost from the server";
        backGamePlay();
    }

    public void startGamePlay()
    {
        panelMultiplayer.transform.localPosition = new Vector3(-1500, 0, 0);
    }

    public void backGamePlay()
    {
        network.StopHost();
        SceneManager.LoadScene("GamePlay");
    }

    public void backToMenu()
    {
        network.StopHost();
    }
}