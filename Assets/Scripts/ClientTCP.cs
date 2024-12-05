using System;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class ClientTCP : MonoBehaviour
{
    private TcpClient client;
    private StreamWriter writer;
    private StreamReader reader;
    public static ClientTCP instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.runInBackground = true;
        Connect("127.0.0.1", 5000);
    }


    public void GameOverMessage() {
        SendMessageToServer($"Over {Players.instance.myId}");
    }
    /*
        public void MovementationMessage(Vector3 newPosition) {
            SendMessageToServer($"MOVE {Players.instance.myId} {newPosition.x} {newPosition.y}");
        }
        */

    public void ShipSelectionMessage(int currentPage)
    {
        SendMessageToServer($"Ship {Players.instance.myId} {currentPage}");
    }

   

    

    public void MessageSetTotalAsters(int c)
    {
        SendMessageToServer($"TA {Players.instance.myId} {c}");
    }

    public void MessageSetAsterCount(int c)
    {
        SendMessageToServer($"AC {Players.instance.myId} {c}");
    }

    public void ReadyMessage() {
        SendMessageToServer($"Ready {Players.instance.myId}");
    }

    

    private void Connect(string server, int port)
    {
        try
        {
            client = new TcpClient();
            client.ReceiveTimeout = 5000; // Timeout de 5 segundos
            client.Connect(server, port);
            NetworkStream stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            Debug.Log("Conectado ao servidor TCP.");
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao conectar ao servidor: " + e.Message);
        }
    }

    private void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToServer("Olá do cliente!");
            }

            if (client != null && client.Available > 0)
            {
                ReceiveMessage();
            }
        
        
        
    }


    public void SendMessageToServer(string message)
    {

        if (client == null || !client.Connected) return;

        writer.WriteLine(message);
        writer.Flush();
    }

    private void ReceiveMessage()
    {
        if (reader == null) return;

        string receivedMessage = reader.ReadLine();
        Debug.Log("Recebido do servidor: " + receivedMessage);

        string[] message = receivedMessage.Split(' ');
        int id;
        switch (message[0])
        {

            case TCPConstants.NEW_PLAYER_CONSTANT:
                if (int.TryParse(message[1], out id))
                {
                    Players.instance.AddPlayer(id);
                }
                break;
            case "Ship":
                int shipNumber;
                int idPlayer;
                Debug.Log("m2 " + message[2]);
                if (int.TryParse(message[1], out idPlayer) && int.TryParse(message[2], out shipNumber))
                {

                    Players.instance.AddShip(idPlayer, shipNumber);
                }
                break;
            case TCPConstants.YOUR_ID:
                if (int.TryParse(message[1], out idPlayer))
                {
                    Players.instance.myId = idPlayer;
                    Players.instance.AddPlayer(idPlayer);
                }
                break;            
            case "TA":
                int c;
                if (int.TryParse(message[1], out id) && int.TryParse(message[2], out c))
                {
                    if (id != Players.instance.myId)
                    //{
                        AsterBars.Instance.SetTotalEnemyAsters(c);
                    //}
                }
                break;
            case "AC":
                if (int.TryParse(message[1], out id) && int.TryParse(message[2], out c))
                {
                    if (id != Players.instance.myId)
                    //{
                        AsterBars.Instance.SetEnemyAsterCount(c);
                    //}
                }
                break;

            case "Start":
                SceneManager.LoadSceneAsync(1);
                break;

            case "Ready":

                if (int.TryParse(message[1], out id)) {
                    if (id == Players.instance.myId) 
                        MainMenu.Instance.playButton.interactable = false;
                }
                break;
            
            case "Over":
                if (int.TryParse(message[1], out id)) {
                    if (id != Players.instance.myId) 
                        GameController.instance.ShowGameOver("Vitória");
                    else 
                        GameController.instance.ShowGameOver("Derrota");
                    
                }
                break;

                
                





        }


    }

    private void OnApplicationQuit()
    {
        if (writer != null) writer.Close();
        if (reader != null) reader.Close();
        if (client != null) client.Close();
    }
}
