using ElenaDalSavio_Prova6.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.Client
{
    public class Menu
    {
        private static MainBL mainBL = new MainBL(new EF.Repositories.EFPolicyRepository(), new EF.Repositories.EFClientRepository());
        internal static void Start()
        {
            Console.WriteLine("Benvenuto!");

            char choice;

            do
            {
                Console.WriteLine("Premi 1 per inserire un nuovo cliente");
                Console.WriteLine("Premi 2 per inserire una polizza per un cliente già esistente");
                Console.WriteLine("Premi 3 per visualizzare le polizze di un cliente");//5
                Console.WriteLine("Premi 4 per posticipare la data di scadenza");


                Console.WriteLine("Premi Q per uscire");

                choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        //Aggiungi cliente
                        Console.WriteLine();
                        AddNewClient();
                        Console.WriteLine();
                        break;
                    case '2':
                        //inserire polizza
                        Console.WriteLine();
                        AddNewPolicy();
                        break;
                    case '3':
                        //visualizzare le polizze di un cliente
                        Console.WriteLine();
                        ShowClientPolicies();
                        Console.WriteLine();
                        break;
                    case '4':
                        //posticipare la data di scadenza
                        Console.WriteLine();
                        UpdatePolicy();
                        Console.WriteLine();
                        break;
                    case 'Q':
                        return;
                    default:
                        Console.WriteLine("Scelta non disponibile");
                        break;
                }
                Console.WriteLine();
            }
            while (!(choice == 'Q'));
        }

        private static void UpdatePolicy()
        {
            var policies = mainBL.FetchPolicies();
            foreach (var p in policies)
            {
                Console.WriteLine($"Numero della polizza: {p.PolicyNumber} Data di scadenza: {p.DueDate} " +
                            $"Rata mensile: {p.MonthlyPayment} Tipologia {(EnumType)p.Type} Id {p.Id}");
            }
            int choice;
            bool check=false;
            do {
                Console.WriteLine("Inserisci il codice id della polizza che vuoi modificare:");
                check = int.TryParse(Console.ReadLine(), out choice);
            } while (check==false);
            Policy policy= GetPolicyById(choice);
            DateTime nuovData= ChiediDate();
            bool IsAdded=mainBL.UpdateDatePolicy(policy, nuovData);
            if (IsAdded)
                Console.WriteLine("Polizza modificata con successo");
            else
                Console.WriteLine("Qualcosa è andato storto");
        }

        private static DateTime ChiediDate()
        {
            DateTime dt = new DateTime();
            DateTime today = DateTime.Today;
            today.ToShortDateString();
            bool isDate;
            do
            {
                Console.WriteLine("Inserisci la nuova data di scadenza");

                isDate = DateTime.TryParse(Console.ReadLine(), out dt);

            } while (!isDate || DateTime.Compare(dt, today) < 0);
            //||today<dt
            return dt;
        }
        private static void ShowClientPolicies()
        {
            string code;
            Core.Models.Client client;
            do
            {
                do
                {
                    Console.Write("\nInserisci il codice fiscale di 16 cifre:");
                    code = Console.ReadLine();
                }
                while (code.Length != 16);
                client = GetClientByCode(code);
            }
            while (client == null);

            var policies = mainBL.FetchByClient(client);

            if (policies.Count != 0)
            {
                Console.WriteLine("Polizze del cliente:");
                foreach (var p in policies)
                {
                    Console.WriteLine($"Numero della polizza: {p.PolicyNumber} Data di scadenza: {p.DueDate} " +
                        $"Rata mensile: {p.MonthlyPayment} Tipologia {(EnumType)p.Type}");
                }
            }
            else
            {
                Console.WriteLine("\nNon ci sono polizze per questo cliente");
            }
        }

        private static void AddNewPolicy()
        {
            int policyNumber;
            DateTime dueDate;
            decimal monthlyRate;
            int type;
            string code;
            bool check;
            Core.Models.Client client;


            do
            {
                do
                {
                    Console.Write("\nInserisci il codice fiscale di 16 cifre:");
                    code = Console.ReadLine();
                }
                while (code.Length != 16);
                do
                {
                    Console.Write("Inserisci numero di polizza:");
                    check = int.TryParse(Console.ReadLine(), out policyNumber);
                }
                while (!check);
                do
                {
                    Console.Write("Inserisci la data di scadenza:");
                    check = DateTime.TryParse(Console.ReadLine(), out dueDate);
                }
                while (!check || DateTime.Today > dueDate);
                do
                {
                    Console.Write("Inserisci la rata mensile:");
                    check = decimal.TryParse(Console.ReadLine(), out monthlyRate);
                }
                while (!check);
                do
                {
                    Console.Write("Inserisci la tipolgia:");

                    foreach (var types in Enum.GetValues(typeof(EnumType)))
                    {
                        Console.WriteLine($"Premi {(int)types} per {(EnumType)types}");
                    }
                    check = int.TryParse(Console.ReadLine(), out type);
                }
                while (!check || type < 0 || type > 4);
                client = GetClientByCode(code);
            } while (client == null);
            Policy newpolicy = new Policy
            {
                Client = client,
                PolicyNumber = policyNumber,
                DueDate = dueDate,
                Type = (EnumType)type,
                MonthlyPayment = monthlyRate,
                ClientId = client.Id

            };
            bool IsAdded = mainBL.AddPolicy(newpolicy);
            if (IsAdded)
                Console.WriteLine("Polizza aggiunta con successo");
            else
                Console.WriteLine("Qualcosa è andato storto");

        }

        private static void AddNewClient()
        {
            string name = null;
            string lastName = null;
            string code;
            do
            {
                Console.Write("\nInserisci il codice fiscale di 16 caratteri:");
                code = Console.ReadLine();
            }
            while (code.Length != 16);
            if (GetClientByCode(code) == null)
            {
                do
                {
                    Console.Write("Inserisci il nome:");
                    name = Console.ReadLine();
                }
                while (name.Length == 0);

                do
                {
                    Console.Write("Inserisci il cognome:");
                    lastName = Console.ReadLine();
                } while (lastName.Length == 0);
            }
            Core.Models.Client newclient = new Core.Models.Client
            {
                Code = code,
                Name = name,
                LastName = lastName
            };
            bool IsAdded = mainBL.AddClient(newclient);
            if (IsAdded)
                Console.WriteLine("Cliente aggiunto con successo");
            else
                Console.WriteLine("Qualcosa è andato storto");
        }
        private static Core.Models.Client GetClientByCode(string code)
        {
            Core.Models.Client client = mainBL.GetByCode(code);

            return client;
        }
        private static Policy GetPolicyById(int choice)
        {
            Policy policy = mainBL.GetById(choice);
            return policy;
        }
    }
    }
