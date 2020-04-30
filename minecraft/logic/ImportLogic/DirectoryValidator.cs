using System.IO;

namespace nihilus.Logic.ImportLogic
{
    public static class DirectoryValidator
    {
        public static ServerValidationInfo ValidateServerDirectory(DirectoryInfo dirInfo)
        {
            ServerValidationInfo serverValInfo = new ServerValidationInfo();

            if (dirInfo == null || !dirInfo.Exists)
            {
                return serverValInfo;
            }

            foreach (DirectoryInfo directory in dirInfo.EnumerateDirectories())
            {
                WorldValidationInfo info = ValidateWorldDirectory(directory);
                if (info.IsValid)
                {
                    serverValInfo.WorldValidationInfo = info;
                }
            }

            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                string name = file.Name;
                if (name.Equals("server.properties"))
                {
                    serverValInfo.ServerProperties = true;
                } else if (name.Equals("eula.txt"))
                {
                    serverValInfo.EulaTxt = true;
                } else if (name.Equals("whitelist.json") || name.Equals("white-list.txt"))
                {
                    serverValInfo.Whitelist = true;
                } else if (name.Equals("ops.json") || name.Equals("ops.txt"))
                {
                    serverValInfo.Ops = true;
                } else if (name.Equals("banned-ips.json") || name.Equals("banned-players.json") 
                                                          || name.Equals("banned-ips.txt") 
                                                          || name.Equals("banned-players.txt"))
                {
                    serverValInfo.Banlist = true;

                } else if (name.Equals("bukkit.yml"))
                {
                    serverValInfo.BukkitYml = true;
                }else if (name.Equals("paper.yml"))
                {
                    serverValInfo.PaperYml = true;
                }else if (name.Equals("spigot.yml"))
                {
                    serverValInfo.SpigotYml = true;
                }
            }

            return serverValInfo;
        }
        
        public static WorldValidationInfo ValidateWorldDirectory(DirectoryInfo dirInfo)
        {
            WorldValidationInfo worldValInfo = new WorldValidationInfo();
            
            if (dirInfo == null || !dirInfo.Exists)
            {
                return worldValInfo;
            }

            foreach (DirectoryInfo dir in dirInfo.EnumerateDirectories())
            {
                if (dir.Name.Equals("data"))
                {
                    worldValInfo.Data = true;
                } else if (dir.Name.Equals("region"))
                {
                    worldValInfo.Region = true;
                } else if(dir.Name.Equals("players")|| dir.Name.Equals("playerdata"))
                {
                    worldValInfo.PlayerInfo = true;
                }
            }

            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                if (file.Name.Equals("level.dat"))
                {
                    worldValInfo.LevelDat = true;
                } else if (file.Name.Equals("session.lock"))
                {
                    worldValInfo.SessionLock = true;
                }
            }

            return worldValInfo;
        }
    }
    
    public class WorldValidationInfo
    {
        public bool Data { get; set; }
        public bool Region { get; set; }
        public bool LevelDat { get; set; }
        public bool SessionLock { get; set; }
        public bool PlayerInfo { get; set; }

        public bool IsValid => Data && Region && LevelDat;
    }

    public class ServerValidationInfo
    {
        public WorldValidationInfo WorldValidationInfo { get; set; } = new WorldValidationInfo();
        public bool ServerProperties { get; set; }
        public bool EulaTxt { get; set; }
        public bool Whitelist { get; set; }
        public bool Ops { get; set; }
        public bool Banlist { get; set; }
        public bool BukkitYml { get; set; }
        public bool PaperYml { get; set; }
        public bool SpigotYml { get; set; }
        public bool IsValid => WorldValidationInfo.IsValid;
    }
}