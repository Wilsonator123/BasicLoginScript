bool exit = false;
while (!exit)
{
    Console.WriteLine("""
                      1. Login
                      2. Sign Up
                      x. Exit
                      
                      """);
    string? userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            Login();
            break;
        case "2":
            SignUp();
            break;
        case "x":
            Console.WriteLine("Exiting...");
            exit = true;
            break;
        default:
            Console.WriteLine("Exiting...");
            exit = true;
            break;
    }
}

//Logging in user
static void Login()
{
    //Enter Deatils
    Console.WriteLine("Enter a username: ");
    string? inputName = Console.ReadLine();
    Console.WriteLine("Enter your password: ");
    string? inputPassword = Console.ReadLine();

    //Check details aren't null
    if (inputName == "" || inputPassword == "")
    {
        Console.WriteLine("Invalid username or password");
        return;
    }

    //Check if user exists and password is correct
    string valid = IsValidUser(inputName, inputPassword) ? $"Welcome back {inputName}" : "Incorrect username or password";

    Console.WriteLine(valid);
    Console.WriteLine();
}


//Signing user up
static bool SignUp()
{
    //Enter a unique username
    Console.WriteLine("Please enter a username:");
    string? inputName = Console.ReadLine();
    while (inputName == "" || IsValidUser(inputName, null))
    {
        Console.WriteLine("Username taken! Please enter another username:");
        inputName = Console.ReadLine();
    }

    //Enter the password and confirm
    Console.WriteLine($"Hi, {inputName}, enter your password:");
    string? inputPassword = Console.ReadLine();
    Console.WriteLine("Please confirm your password");
    string? confirmedPassword = Console.ReadLine();

    //If passwords don't match ask again
    while (inputPassword != confirmedPassword)
    {
        Console.WriteLine("Passwords don't match! Please confirm your password");
        confirmedPassword = Console.ReadLine();
    }
    
    //Create user
    CreateUser(inputName, inputPassword);

    return true;
}

//Returns true if user exists and password correct (if parsed)
static bool IsValidUser(string username, string? password)
{
    //Find the user
    string[] user = FindUser(username);

    //If the user doesn't exist
    if (user[0] == "") return false;

    //If password given, does it match
    if (password is not null) return password == user[1];

    return true;
}


//Finding and returning user (if exists)
    static string[] FindUser(string username)
    {
        //Easier than ReadLines which returns as string
        string[] users = File.ReadAllLines("users.txt");

        //For each user in the file -> Can we simplify this?
        foreach (string line in users)
        {
            //Split the user [username, password]
            string[] user = line.Split(",");
            //If the name is the one we are searching for
            if (user[0] == username)
                return user;
        }

        //No user found -> What would be a better return?
        return ["", ""];
    }

    static void CreateUser(string username, string password)
    {
        //Add new user to the users.txt file
        File.AppendAllText("./users.txt", $"{username},{password}\n");
        Console.WriteLine("User Created! \n");
        return;
    }
