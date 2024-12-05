using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Threading;
using Newtonsoft.Json;

class TcpServer
{
    private TcpListener server;

    // Lista de clientes conectados

    Dictionary<TcpClient, Player> clientesConectados = new Dictionary<TcpClient, Player>();
    Dictionary<int, Player> players = new Dictionary<int, Player>();

    const string ROTATE_CONST = "R";
    const string NEW_PLAYER_CONSTANT = "NP";
    const string YOUR_ID = "YI";
    int id = 0;

  

    private AstersController astersController = new AstersController();



    public void StartServer(int port)
    {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Console.WriteLine("Servidor TCP iniciado. Aguardando conexões...");
        //generateAstersThread = new Thread(() => GenerateAsters());
        //generateAstersThread.Start();
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Player p = new Player();
            p.Id = id++;
            p.ready = false;
            players.Add(p.Id, p);
            clientesConectados.Add(client, p);
            Console.WriteLine("Cliente conectado.");
            Thread clientThread = new Thread(() => HandleClient(client));


            clientThread.Start();
        }
    }



    private void HandleClient(TcpClient client)
    {

        NetworkStream stream = client.GetStream();
        StreamWriter writer = new StreamWriter(stream);
        StreamReader reader = new StreamReader(stream);


        string m1 = YOUR_ID + " " + clientesConectados[client].Id;

        Console.WriteLine("Enviado para o cliente " + clientesConectados[client].Id + ": " + m1);

        // Envia uma mensagem inicial de boas-vindas
        writer.WriteLine(m1);
        writer.Flush();

        foreach (var cliente in clientesConectados.Keys)
        {
            if (clientesConectados[cliente].Id  < clientesConectados[client].Id) {
                StreamWriter writer2 = new StreamWriter(client.GetStream());
                string m = NEW_PLAYER_CONSTANT + " " + clientesConectados[cliente].Id;
                Console.WriteLine("Enviado para o cliente " + clientesConectados[client].Id + ": " + m);
                writer2.WriteLine(m);
                writer2.Flush();
                string message2 = "Ship" + " " + clientesConectados[cliente].Id + " " + clientesConectados[cliente].ship;
                Console.WriteLine("Enviado para o cliente "+ clientesConectados[client].Id +": "+ message2);
                writer2.WriteLine(message2);
                writer2.Flush();

            }
        }



            foreach (var cliente in clientesConectados.Keys)
            {
            if (cliente != client && cliente.Connected)
            {
                StreamWriter writer2 = new StreamWriter(cliente.GetStream());
                string message2 = NEW_PLAYER_CONSTANT + " " + clientesConectados[client].Id;
                Console.WriteLine("Enviado para o cliente "+ clientesConectados[cliente].Id + ": " + message2);
                writer2.WriteLine(message2);
                writer2.Flush();
            }
        }

        while (client.Connected)
        {
            try
            {
                string message = reader.ReadLine();
                if (message != null)
                {
                    Console.WriteLine("Recebido do cliente: " + message);
                    string[] subs = message.Split(' ');

                   // if (subs[0] == "TA") {
                   //     message = $"TA {subs[1]} {randomScore(subs)}";
                   // } else if (subs[0] == "AC") {
                   //     message = $"AC {subs[1]} {randomScore(subs)}";
                   // }
                    
                    switch (subs[0])
                    {
                       
                        case "Ready":
                            int idPlayer;
                            if (int.TryParse(subs[1], out idPlayer))
                            {
                                players[idPlayer].ready = true;

                                if (CheckPlayers()) {
                                    string readyMessage = "Start";
                                    BroadcastMessageToAllClients(readyMessage);
                                }

                            }
                        break;

                        case "AC":
                            FowardMessage(clientesConectados[client], message);
                            int c;
                            int idPlayer2;
                            float dif;
                            if (int.TryParse(subs[1], out id) && int.TryParse(subs[2], out c)) {
                                players[id].asterCount = c;
                                foreach (var p in players.Keys) {
                                    if (p != id) {
                                        idPlayer2 = p;
                                        float a = (float) (players[id].asterCount - players[idPlayer2].asterCount)/(players[id].totalAsters+players[id].totalAsters);
                                        float b = (float) (-players[id].asterCount + players[idPlayer2].asterCount)/(players[id].totalAsters+players[id].totalAsters);
                                        dif = a - b;
                                        if (dif > 0.2f) {
                                            Console.WriteLine("Asteroid vermelho gerado no mapa do player " + idPlayer2);
                                        } else if (dif < -0.2f) {
                                            Console.WriteLine("Asteroid vermelho gerado no mapa do player " + id);
                                        }
                                        
                                    }
                                }
                            }
                                break;
                        case "TA":
                            
                            FowardMessage(clientesConectados[client], message);
                            if (int.TryParse(subs[1], out id) && int.TryParse(subs[2], out c))
                            {
                                players[id].totalAsters = c;
                            }
                            break;
                        case "Ship":
                            int shipNumber;
                            //int idPlayer;
                            //Debug.Log("m2 " + message[2]);
                            BroadcastMessageToAllClients(message);
                            if (int.TryParse(subs[1], out idPlayer) && int.TryParse(subs[2], out shipNumber)) {
                                players[idPlayer].ship = shipNumber;

                            }
                            break;
                        case "Over":
                            FowardMessage(clientesConectados[client], message);
                            ResetState();

                            break;
                        




                    }

                    

                    writer.WriteLine(message);
                    writer.Flush();

                    


                    



                }
            }
            catch
            {
                
                Console.WriteLine("Cliente desconectado.");
                break;
            }
        }

        // Fecha o stream e o cliente quando a conexão termina
        writer.Close();
        reader.Close();
        client.Close();
    }

    void ResetState() {
        foreach (var p in players.Values) {
            p.ready = false;
            p.asterCount = 0;
            p.totalAsters = 0;
        }
    }

    bool CheckPlayers() {
        bool check = true;
        foreach (var p in players.Values) { 
            if (!p.ready) { 
                check = false;
                break;
            }
        }
        
        return check;
    }


    void FowardMessage(Player player, string message) {
        foreach (var client in clientesConectados.Keys)
        {
            if (player.Id != clientesConectados[client].Id) {
                try
                {
                    NetworkStream stream = client.GetStream();
                    StreamWriter writer = new StreamWriter(stream);
                    Console.WriteLine("Enviado ao cliente " + clientesConectados[client].Id + ": " + message + " (others)");
                    writer.WriteLine(message);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar mensagem para um cliente: {ex.Message}");
                }
            }
           
            
        }

    }


    void BroadcastMessageToAllClients(string message)
    {
        foreach (var client in clientesConectados.Keys)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                Console.WriteLine("Enviado ao cliente "+ clientesConectados[client].Id + ": " + message + " (Broadcast)");
                writer.WriteLine(message);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem para um cliente: {ex.Message}");
            }
        }
    }





    public static void Main()
    {
        TcpServer server = new TcpServer();
        server.StartServer(5000);
    }
}


class Player
{
    public int Id { get; set; }
    public int ship { get; set; }
    public bool ready { get; set; }
    public int asterCount { get; set; }
    public int totalAsters { get; set; }
}