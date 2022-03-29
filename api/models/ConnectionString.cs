namespace api.models
{
    public class ConnectionString
{
    public string cs;
    public string server;
    public string database;
    public string port;
    public string uid;
    public string pwd;

    public ConnectionString()
    {
        server = "phtfaw4p6a970uc0.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
        database = "y097gcl63fe0z8x0";
        uid = "a4yzcqvmamu249k3";
        port = "3306";
        pwd = "d4mdjaab0b2jpl0y";

        cs = $"server={server};database={database};uid={uid};pwd={pwd};";
    }
}
}