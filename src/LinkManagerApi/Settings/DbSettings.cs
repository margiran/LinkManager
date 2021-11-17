namespace LinkManagerApi.Settings;
public class DbSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get
        {
            return $"Server={Host},{Port};Initial Catalog=LinkManagerDB;User Id={User};Password={Password}";
        }
    }
}


// "ConnectionStrings": {
//   "default": "Server=localhost,1433;Initial Catalog=LinkManagerDB;User Id=sa;Password="
// },