using System;
using DnsClient;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        LookupClient dnsClient = new LookupClient(); 
        string dnsServer = null;

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Search by IP");
            Console.WriteLine("2. Search by name");
            Console.WriteLine("3. Select another DNS server"); 
            Console.WriteLine("4. Quit");

            Console.Write("Enter an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the IP address to search: ");
                    string inputIp = Console.ReadLine();


                    if (dnsServer != null)
                    {
                        var endpoint = new IPEndPoint(IPAddress.Parse(dnsServer), 53);
                        dnsClient = new LookupClient(endpoint);
                    }

                    IPAddress ipAddress;
                    if (IPAddress.TryParse(inputIp, out ipAddress)) 
                    {
                        Console.WriteLine("IP detected");

                        var result = dnsClient.GetHostEntry(ipAddress);

                        //var item = result.AddressList.FirstOrDefault<IPAddress>(u => IPAddress.Equals(u.Address));

                        Console.WriteLine($"{result.HostName.Substring(result.HostName.IndexOf('.') + 1)}");
                        //Console.WriteLine($"{item}");
                    }

                    else
                    {
                        Console.WriteLine("Invalid format.");
                    }

                    break;

                case "2":

                    Console.Write("Enter the name to search: ");
                    string query = Console.ReadLine();

                    if (dnsServer != null)
                    {
                        var endpoint = new IPEndPoint(IPAddress.Parse(dnsServer), 53);
                        dnsClient = new LookupClient(endpoint);
                    }

                    var result1 = dnsClient.Query(query, QueryType.A); 
                    var records1 = result1.Answers;

                    if (records1.Count > 0)
                    {
                        Console.WriteLine($"Found {records1.Count} records:");
                        foreach (var record in records1)
                        {
                            Console.WriteLine(record);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found.");
                    }
                     break;

                case "3":

                    Console.Write("Enter the DNS server IP: ");
                    dnsServer = Console.ReadLine();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

           
            Console.WriteLine();
        }
    }
}
