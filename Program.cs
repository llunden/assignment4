using Clients;
Client clienDtls = new();
List<Client> clientsList = [];
bool isTrue = true;
LoadFileToMemory(clientsList);
while (isTrue)
{
  try
  {
    displayMainMenu();
    string mainMenuChoice = Prompt("\nEnter a Menu Choice: ").ToUpper();
    if (mainMenuChoice == "L")
      displayClientList();
    if (mainMenuChoice == "F")
      findClient();
    if (mainMenuChoice == "A")
      AddClientToList(clientsList);
    if (mainMenuChoice == "E")
      EditClient();
    if (mainMenuChoice == "D")
      DeleteClient();
    if (mainMenuChoice == "S")
      showClientBmiInfo();
    if (mainMenuChoice == "Q")
    {
      SaveClients();
      isTrue = false;
      throw new Exception("Come back again.");
    }

  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
  }
}

void displayMainMenu()
{
  Console.WriteLine($"\n MENU OPTIONS");
  Console.WriteLine($"(L)ist All Clients");
  Console.WriteLine($"(F)ind Client Information");
  Console.WriteLine($"(A)dd New Client Record");
  Console.WriteLine($"(E)dit Client Information");
  Console.WriteLine($"(D)elete Client Record");
  Console.WriteLine($"(S)how Client BMI Information");
  Console.WriteLine($"(Q)uit");
}

void displayEditMenu()
{
  Console.WriteLine($"(F)irst Name");
  Console.WriteLine($"(L)ast Name");
  Console.WriteLine($"(H)eight");
  Console.WriteLine($"(W)eight");
  Console.WriteLine($"(R)eturn to Main Menu");
}

string Prompt(string prompt)
{
  string myString = "";
  while (true)
  {
    try
    {
      Console.Write(prompt);
      myString = Console.ReadLine().Trim();
      if (string.IsNullOrEmpty(myString))
        throw new Exception($"Empty Input: Please enter something.");
      break;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
  return myString;
}


double PromptDoubleBetweenMin(String msg, double min)
{
  double num = 0;
  while (true)
  {
    try
    {
      Console.Write($"{msg} between {min}: ");
      num = double.Parse(Console.ReadLine());
      if (num < min)
        throw new Exception($"Must be greater than {min:n2}");
      break;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Invalid: {ex.Message}");
    }
  }
  return num;
}

void LoadFileToMemory(List<Client> clientsList)
{
  while (true)
  {
    try
    {
      //string fileName = Prompt("Enter file name including .csv or .txt: ");
      string fileName = "client.csv";
      string filePath = $"./data/{fileName}";
      if (!File.Exists(filePath))
        throw new Exception($"The file {fileName} does not exist.");
      string[] csvFileInput = File.ReadAllLines(filePath);
      for (int i = 0; i < csvFileInput.Length; i++)
      {
        //Console.WriteLine($"lineIndex: {i}; line: {csvFileInput[i]}");
        string[] items = csvFileInput[i].Split(',');
        for (int j = 0; j < items.Length; j++)
        {
          //Console.WriteLine($"itemIndex: {j}; item: {items[j]}");
        }
        Client clienDtls = new(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
        clientsList.Add(clienDtls);
      }
      Console.WriteLine($"Load complete. {fileName} has {clientsList.Count} data entries");
      break;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}

void displayClientList()
{
  try
  {
    if (clientsList.Count <= 0)
      throw new Exception($"No data has been loaded");

    foreach (Client clienDtls in clientsList)
      showClientInfo(clienDtls);
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Error: {ex.Message}");
  }
}

void findClient()
{
  displayClientList();
  string clienDtlsName = Prompt("Enter clienDtls's Firstname: ");
  List<Client> filteredClient = clientsList.Where(c => c.Firstname.Contains(clienDtlsName)).ToList();

  Client selClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selClient.BmiScore:n2}");
  Console.WriteLine($"Client's BMI Status    :\t{selClient.BmiStatus}");

}



void AddClientToList(List<Client> clientsList)
{
  GetFirstname(clienDtls);
  GetLastname(clienDtls);
  GetWeight(clienDtls);
  GetHeight(clienDtls);
  clientsList.Add(clienDtls);
}

void GetFirstname(Client clienDtls)
{
  string myString = Prompt($"Enter Firstname: ");
  clienDtls.Firstname = myString;
}

void GetLastname(Client clienDtls)
{
  string myString = Prompt($"Enter Lastname: ");
  clienDtls.Lastname = myString;
}

void GetWeight(Client clienDtls)
{
  double myDouble = PromptDoubleBetweenMin("Enter Weight in inches: ", 0);
  clienDtls.Weight = myDouble;
}

void GetHeight(Client clienDtls)
{
  double myDouble = PromptDoubleBetweenMin("Enter Height in inches: ", 0);
  clienDtls.Height = myDouble;
}


void showClientBmiInfo()
{
  string clienDtlsName = Prompt("\nEnter clienDtls's Firstname: ");
  List<Client> filteredClient = clientsList.Where(c => c.Firstname.Contains(clienDtlsName)).ToList();
  Client selClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selClient.BmiScore:n2}");
  Console.WriteLine($"Client's BMI Status    :\t{selClient.BmiStatus}");
}



void showClientInfo(Client clienDtls)
{
  if (clienDtls == null)
    throw new Exception("No Client In Memory");
  Console.WriteLine($"\n{clienDtls.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{clienDtls.BmiScore:n2}");
  Console.WriteLine($"Client's BMI Status    :\t{clienDtls.BmiStatus}");
}



void EditClient()
{

  displayClientList();
  string clienDtlsName = Prompt("Enter clienDtls's Firstname: ");
  List<Client> filteredClient = clientsList.Where(c => c.Firstname.Contains(clienDtlsName)).ToList();
  Client selClient = filteredClient.FirstOrDefault();

  while (true)
  {
    Console.WriteLine($"SELECT DATA OF TO EDIT");
    displayEditMenu();
    string editChoice = Prompt("\nEnter Edit Menu Choice: ").ToUpper();
    if (editChoice == "F")
    {
      selClient.Firstname = Prompt($"Enter Client Firstname: ");
    }
    else if (editChoice == "L")
    {
      selClient.Lastname = Prompt($"Enter Client Lastname: ");
    }
    else if (editChoice == "W")
    {
      selClient.Weight = PromptDoubleBetweenMin($"Enter Client Weight (lbs): ", 0);
    }
    else if (editChoice == "H")
    {
      selClient.Height = PromptDoubleBetweenMin($"Enter Client Height (inches): ", 0);
    }
    else if (editChoice == "R")
    {
      Console.WriteLine($"You have successfully updated details");
      break;
    }
    else
    {
      throw new Exception("Invalid Edit Menu Choice. Please Try Again.");
    }
  }
}



void DeleteClient()
{
  displayClientList();
  string clienDtlsName = Prompt("Enter clienDtls's Firstname: ");
  List<Client> filteredClient = clientsList.Where(c => c.Firstname.Contains(clienDtlsName)).ToList();
  Client selClient = filteredClient.FirstOrDefault();

  while (true)
  {
    string confirmation = Prompt($"You are about to delete " + selClient.Firstname + "'s record. Proceed? Y/N: ").ToUpper();
    if (confirmation == "Y")
    {
      clientsList.Remove(selClient);
      Console.WriteLine($"{selClient.Firstname}'s has been deleted.");
      break;
    }
    else if (confirmation == "N")
    {
      Console.WriteLine($"Delete operation cancelled for {selClient.Firstname}.");
      break;
    }
    else
    {
      Console.WriteLine($"Invalid confirmation input. Please enter 'Y' or 'N'.");
    }
  }
}


void SaveClients()
{
  string fileName = "clienDtls.csv";
  string filePath = $"./data/{fileName}";
  List<String> ClientRecords = [];
  foreach (Client data in clientsList)
  {
    ClientRecords.Add($"{data.Firstname}, {data.Lastname}, {data.Weight}, {data.Height}");
  }
  File.WriteAllLines(filePath, ClientRecords);
}