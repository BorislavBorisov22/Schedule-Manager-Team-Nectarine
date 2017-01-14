using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TeamNectarineScheduleManager.Users;
using TeamNectarineScheduleManager.Teams;

namespace TeamNectarineScheduleManager.DataBaseLibrary
{
    public static class DataBase
    {
        //public static List<Worker> GetAllWorkersPending() returns a list of all of the workers on disc with pending calendars
        //public static void MigrateDataBase(string newDirectory) also check if there is already such a directory and if there are already files with same names in it.

        // Fields
        #region
        private static string dataDir = "Data";
        private static string custDir = "Data\\Custom";
        private static Exception excFileNotFound = new Exception("No file corresponds to the given input.");
        private static Exception excIOError = new Exception("Input/Output error.");
        private static Exception excNullReference = new Exception("Null reference.");
        #endregion

        // Methods
        #region

        #region Main

        #region General

        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        /// <param name="userName"></param>
        public static void DeleteUser(string userName)
        {
            try
            {
                if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
                {
                    File.Delete(string.Format("{0}\\A{1}.bin", dataDir, userName));
                }
                if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
                {
                    File.Delete(string.Format("{0}\\W{1}.bin", dataDir, userName));
                }
                if (File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName)))
                {
                    File.Delete(string.Format("{0}\\L{1}.bin", dataDir, userName));
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        #endregion // End of Main.General

        #region RegularWorker

        /// <summary>
        /// Saves a regular worker object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void Save(RegularWorker regularWorker)
        {
            if (object.ReferenceEquals(regularWorker, null))
            { }
            try
            {
                // Create a deep copy and prepare it for serialization.
                RegularWorker internalWorker = DeepCopy(regularWorker);
                internalWorker.NFDBPasswordCheckBypass = Encryptor.EncryptString(internalWorker.Password);
                internalWorker.NFDBSetTeamName();
                internalWorker.NFDBTeamBypass = null;
                // End of preparation

                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create corresponding file name.

                string fileName = string.Format("{0}\\W{1}.bin", dataDir, internalWorker.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, internalWorker);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads a regular worker object from the disc.
        /// </summary>
        /// <param name="userName">The name of the regular worker.</param>
        /// <returns></returns>
        public static RegularWorker LoadRegularWorker(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameWorker = string.Format("{0}\\W{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameWorker))
                {
                    using (Stream stream = File.OpenRead(fileNameWorker))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        RegularWorker result = (RegularWorker)formatter.Deserialize(stream);
                        stream.Close();

                        // Decrypt Password
                        result.NFDBPasswordCheckBypass = Encryptor.DecryptString(result.NFDBPasswordCheckBypass);
                        // Attach the team.
                        result.NFDBTeamBypass = LoadTeam(result.NFDBGetTeamName());

                        // Replace the mirror object in the team with this object
                        result.NFDBTeamBypass = ReplaceRef(result.NFDBTeamBypass, result);

                        return result;
                    }                   
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }
        
        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        public static void DeleteUser(RegularWorker user)
        {
            DeleteUser(user.Username);
        }

        #endregion  // End of Main.RegularWorker

        #region TeamLeader

        /// <summary>
        /// Saves a team leader worker object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void Save(TeamLeaderWorker teamLeader)
        {
            if (object.ReferenceEquals(teamLeader, null))
            { }
            try
            {
                // Create a deep copy and prepare it for serialization.
                TeamLeaderWorker internalTeamLeader = DeepCopy(teamLeader);
                internalTeamLeader.NFDBPasswordCheckBypass = Encryptor.EncryptString(internalTeamLeader.NFDBPasswordCheckBypass);
                internalTeamLeader.NFDBSetTeamName();
                internalTeamLeader.NFDBTeamBypass = null;
                // End of preparation

                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create corresponding file name.

                string fileName = string.Format("{0}\\L{1}.bin", dataDir, internalTeamLeader.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, internalTeamLeader);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads a team leader worker object from the disc.
        /// </summary>
        /// <param name="userName">The name of the team leader worker.</param>
        /// <returns></returns>
        public static TeamLeaderWorker LoadTeamLeader(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameLeader = string.Format("{0}\\L{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameLeader))
                {                    
                    using (Stream stream = File.OpenRead(fileNameLeader))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        TeamLeaderWorker result = (TeamLeaderWorker)formatter.Deserialize(stream);
                        stream.Close();

                        // Decrypt password
                        result.NFDBPasswordCheckBypass = Encryptor.DecryptString(result.NFDBPasswordCheckBypass);

                        // Attach the team.
                        result.NFDBTeamBypass = LoadTeam(result.NFDBGetTeamName());

                        // Replace the mirror object in the team with this object
                        result.NFDBTeamBypass = ReplaceRef(result.NFDBTeamBypass, result);

                        return result;
                    }
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        public static void DeleteUser(TeamLeaderWorker user)
        {
            DeleteUser(user.Username);
        }

        #endregion  // End of Main.TeamLeader

        #region Administrator

        /// <summary>
        /// Saves an admin object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void WriteToDisc(Administrator administrator)
        {
            try
            {
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create a deep copy and prepare it for serialization.
                Administrator internalAdministrator = DeepCopy(administrator);
                internalAdministrator.NFDBPasswordCheckBypass = Encryptor.EncryptString(internalAdministrator.NFDBPasswordCheckBypass);
                // End of preparation

                // Create corresponding file name.
                string fileName = string.Format("{0}\\A{1}.bin", dataDir, internalAdministrator.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, internalAdministrator);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads an admin object from the disc.
        /// </summary>
        /// <param name="userName">The name of the admin.</param>
        /// <returns></returns>
        public static Administrator ReadFromDiscAdmin(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameAdmin = string.Format("{0}\\A{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameAdmin))
                {
                    using (Stream stream = File.OpenRead(fileNameAdmin))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Administrator result = (Administrator)formatter.Deserialize(stream);
                        stream.Close();

                        // Decrypt password
                        result.NFDBPasswordCheckBypass = Encryptor.DecryptString(result.NFDBPasswordCheckBypass);

                        return result;
                    }
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        public static void DeleteUserFromDisc(Administrator user)
        {
            DeleteUser(user.Username);
        }

        #endregion  // End of Main.Administrator

        #region Team
        /// <summary>
        /// Saves a team object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void Save(Team team)
        {
            try
            {
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create a deep copy and prepare it for serialization.
                Team internalTeam = DeepCopy(team);
                internalTeam.NFDBSetTeamLeaderName();
                internalTeam.NFDBSetRegularWorkerNames();
                internalTeam.NFDBClearMembersAndLeader();
                // End of preparation

                // Create corresponding file name.
                string fileName = string.Format("{0}\\T{1}.bin", dataDir, internalTeam.TeamName);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, internalTeam);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads a team object from the disc.
        /// </summary>
        /// <param name="userName">The name of the admin.</param>
        /// <returns></returns>
        public static Team LoadTeam(string teamName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameTeam = string.Format("{0}\\T{1}.bin", dataDir, teamName);
                // Check if the file exists;
                if (File.Exists(fileNameTeam))
                {
                    using (Stream stream = File.OpenRead(fileNameTeam))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Team result = (Team)formatter.Deserialize(stream);
                        stream.Close();

                        // Attach Users
                        result.TeamLeader = LoadTeamLeaderNoTeam(result.NFDBGetTeamLeaderName());

                        List<string> regularWorkerNames = result.NFDBGetRegularWorkerNames();
                        for (int i = 0; i < regularWorkerNames.Count; i++)
                        {
                            result.AddMember(LoadRegularWorkerNoTeam(regularWorkerNames[i]));
                        }

                        //Attach the team to the added users.

                        //result.NFDBTeamLeaderBypass.NFDBTeamBypass = result;
                        //for (int i = 0; i < result.Members.Count; i++)
                        //{
                        //    result.Members[3]
                        //}

                        return result;
                    }
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Deletes the file, storing this team.
        /// </summary>
        public static void DeleteTeam(Team team)
        {
            DeleteTeam(team.TeamName);
        }

        /// <summary>
        /// Deletes the file, storing this team.
        /// </summary>
        public static void DeleteTeam(string teamName)
        {
            try
            {
                if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, teamName)))
                {
                    File.Delete(string.Format("{0}\\A{1}.bin", dataDir, teamName));
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        #endregion

        #region Generic
        /// <summary>
        /// Saves any object to the hard drive. If a file with the given name already exists it will be replaced.
        /// </summary>
        /// <param name="obj">The object to be saved.</param>
        /// <param name="fileName">The name of the file.</param>
        public static void SaveGeneric(object obj, string fileName)
        {
            try
            {
                if (!Directory.Exists(custDir))
                {
                    Directory.CreateDirectory(custDir);
                }
                // Create corresponding file name.

                string fullFileName = string.Format("{0}\\{1}", custDir, fileName);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fullFileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads any object from the hard drive.
        /// </summary>
        /// <param name="fileName">The name of the file, where the object is saved.</param>
        /// <returns></returns>
        public static object LoadGeneric(string fileName)
        {
            try
            {
                // Create corresponding file name.
                fileName = string.Format("{0}\\{1}", custDir, fileName);
                // Check if the file exists;
                if (File.Exists(fileName))
                {
                    Stream stream = File.OpenRead(fileName);
                    BinaryFormatter formatter = new BinaryFormatter();
                    object result = (object)formatter.Deserialize(stream);
                    stream.Close();
                    return result;
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        #endregion  // End of Main.Generic

        #endregion // End of Main region

        #region Secondary

        /// <summary>
        /// Checks if a user with the given name exists. Returns true if it does, and false if it doesn't.
        /// </summary>
        /// <param name="userName">The name of the user to check.</param>
        /// <returns></returns>
        public static bool UserExists(string userName)
        {
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName))
                || File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName))
                || File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a list of all of the workers, that are saved on the disc.
        /// </summary>
        /// <returns></returns>
        public static List<RegularWorker> GetAllRegularWorkers()
        {
            string[] files = Directory.GetFiles(dataDir);
            List<RegularWorker> workers = new List<RegularWorker>();
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    Stream stream = File.OpenRead(files[i]);
                    BinaryFormatter formatter = new BinaryFormatter();
                    RegularWorker result = (RegularWorker)formatter.Deserialize(stream);
                    stream.Close();
                    workers.Add(result);
                }
                catch { }
            }
            return workers;
        }

        /// <summary>
        /// Returns a list of all of the admins, that are saved on the disc.
        /// </summary>
        /// <returns></returns>
        public static List<Administrator> GetAllAdmins()
        {
            string[] files = Directory.GetFiles(dataDir);
            List<Administrator> admins = new List<Administrator>();
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    Stream stream = File.OpenRead(files[i]);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Administrator result = (Administrator)formatter.Deserialize(stream);
                    stream.Close();
                    admins.Add(result);
                }
                catch { }
            }
            return admins;
        }

        /// <summary>
        /// Returns a list of all of the team leaders, that are saved on the disc.
        /// </summary>
        /// <returns></returns>
        public static List<TeamLeaderWorker> GetAllTeamLeaders()
        {
            string[] files = Directory.GetFiles(dataDir);
            List<TeamLeaderWorker> teamLeaders = new List<TeamLeaderWorker>();
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    Stream stream = File.OpenRead(files[i]);
                    BinaryFormatter formatter = new BinaryFormatter();
                    TeamLeaderWorker result = (TeamLeaderWorker)formatter.Deserialize(stream);
                    stream.Close();
                    teamLeaders.Add(result);
                }
                catch { }
            }
            return teamLeaders;
        }

        /// <summary>
        /// Returns a list of the user names from all of the users, that are saved on the disc.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllUserNames()
        {
            List<RegularWorker> allWorkers = GetAllRegularWorkers();
            List<TeamLeaderWorker> allTeamLeaders = GetAllTeamLeaders();
            List<Administrator> allAdmins = GetAllAdmins();
            List<string> userNames = new List<string>();
            for (int i = 0; i < allWorkers.Count; i++)
            {
                userNames.Add(allWorkers[i].Username);
            }
            for (int i = 0; i < allTeamLeaders.Count; i++)
            {
                userNames.Add(allTeamLeaders[i].Username);
            }
            for (int i = 0; i < allAdmins.Count; i++)
            {
                userNames.Add(allAdmins[i].Username);
            }

            return userNames;
        }

        /// <summary>
        /// Checks if the given login data matches any user, saved on the disc.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns></returns>
        public static bool IsValidLoginData(string userName, string password)
        {
            // check admins.
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
            {
                if (ReadFromDiscAdmin(userName).Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Check wokers.
            else if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
            {
                if (LoadRegularWorker(userName).Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Check team leaders.
            else if (File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName)))
            {
                if (LoadTeamLeader(userName).Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        /// <summary>
        /// Checks whether there is a user with the given name on the disc.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsFreeUserName(string userName)
        {
            if ((!File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
                && (!File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
                && (!File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clears the Users data base from any corrupted user files and non-user files. 
        /// </summary>
        public static void ClearCorruptedUserFiles()
        {
            throw new NotImplementedException();
            //foreach (var fileName in Directory.GetFiles(dataDir))
            //{
            //    try
            //    {
            //        string userName = ExtractUserNameFromFileName(fileName, true);

            //        if (userName[0] == 'A')
            //        {
            //            userName = ExtractUserNameFromFileName(userName, false);
            //            try
            //            {

            //                Administrator admin = ReadFromDiscAdmin(userName);
            //                if (admin.Username != userName)
            //                {
            //                    File.Delete(string.Format("{0}\\A{1}.bin", dataDir, userName));
            //                }
            //            }
            //            catch
            //            {
            //                File.Delete(string.Format("{0}\\A{1}.bin", dataDir, userName));
            //            }
            //        }
            //        else if (userName[0] == 'W')
            //        {
            //            userName = ExtractUserNameFromFileName(userName, false);
            //            try
            //            {
            //                Worker worker = ReadFromDiscWorker(userName);
            //                if (worker.Username != userName)
            //                {
            //                    File.Delete(string.Format("{0}\\W{1}.bin", dataDir, userName));
            //                }
            //            }
            //            catch
            //            {
            //                File.Delete(string.Format("{0}\\W{1}.bin", dataDir, userName));
            //            }
            //        }
            //        else
            //        {
            //            File.Delete(string.Format("{0}\\{1}.bin", dataDir, userName));
            //        }
            //    }
            //    catch { }
            //}
        }

        /// <summary>
        /// Checks whether a user file, corresponding to the given user name is wholesome.
        /// </summary>
        /// <returns></returns>
        public static bool IsUserFileWholesome(string userName)
        {
            // check admins
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
            {
                try
                {
                    ReadFromDiscAdmin(userName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            // Check wokers
            else if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
            {
                try
                {
                    LoadRegularWorker(userName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            // Check team leaders
            else if (File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName)))
            {
                try
                {
                    LoadRegularWorker(userName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a user file, corresponding to the given User is wholesome.
        /// </summary>
        /// <returns></returns>
        public static bool IsUserFileWholesome(Administrator user)
        {
            // check admins
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, user.Username)))
            {
                try
                {
                    ReadFromDiscAdmin(user.Username);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a user file, corresponding to the given User is wholesome.
        /// </summary>
        /// <returns></returns>
        public static bool IsUserFileWholesome(RegularWorker user)
        {
            // check workers
            if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, user.Username)))
            {
                try
                {
                    LoadRegularWorker(user.Username);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether a user file, corresponding to the given User is wholesome.
        /// </summary>
        /// <returns></returns>
        public static bool IsUserFileWholesome(TeamLeaderWorker user)
        {
            // check team leaders
            if (File.Exists(string.Format("{0}\\L{1}.bin", dataDir, user.Username)))
            {
                try
                {
                    LoadRegularWorker(user.Username);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns UserType, matching the user file with the given user name.
        /// </summary>
        /// <returns></returns>
        public static UserType GetUserType(string userName)
        {
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
            {
                return UserType.Admin;
            }
            if (File.Exists(string.Format("{0}\\L{1}.bin", dataDir, userName)))
            {
                return UserType.TeamLeader;
            }
            if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
            {
                return UserType.RegularWorker;
            }
            return UserType.Unknown;
        }

        #endregion  // End of Secondary region

        #region Private
        private static string ExtractUserNameFromFileName(string fileName, bool preserveUserTypeChar)
        {
            if (preserveUserTypeChar == true)
            {
                fileName = fileName.Remove(0, fileName.LastIndexOf('\\') + 1);
            }
            else
            {
                fileName = fileName.Remove(0, fileName.LastIndexOf('\\') + 2);
            }
            if (fileName.LastIndexOf(".bin") != -1)
            {
                fileName = fileName.Remove(fileName.LastIndexOf(".bin"));
            }
            return fileName;
        }

        private static T DeepCopy<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        private static RegularWorker LoadRegularWorkerNoTeam(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameWorker = string.Format("{0}\\W{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameWorker))
                {
                    using (Stream stream = File.OpenRead(fileNameWorker))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        RegularWorker result = (RegularWorker)formatter.Deserialize(stream);
                        stream.Close();

                        // Decrypt password
                        result.NFDBPasswordCheckBypass = Encryptor.DecryptString(result.NFDBPasswordCheckBypass);

                        return result;
                    }
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        private static TeamLeaderWorker LoadTeamLeaderNoTeam(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameLeader = string.Format("{0}\\L{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameLeader))
                {
                    using (Stream stream = File.OpenRead(fileNameLeader))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        TeamLeaderWorker result = (TeamLeaderWorker)formatter.Deserialize(stream);
                        stream.Close();

                        // Decrypt password
                        result.NFDBPasswordCheckBypass = Encryptor.DecryptString(result.NFDBPasswordCheckBypass);

                        return result;
                    }
                }
                else
                {
                    throw excFileNotFound;
                }
            }
            catch
            {
                throw excIOError;
            }
        }

        private static Team ReplaceRef(Team team, Worker worker)
        {
            if (team.NFDBTeamLeaderBypass.Username == worker.Username)
            {
                team.NFDBTeamLeaderBypass = (TeamLeaderWorker)worker;
            }
            int index = - 1;
            for (int i = 0; i < team.NFDBMembersBypass.Count; i++)
            {
                if (team.NFDBMembersBypass[i].Username == worker.Username)
                {
                    index = i;
                    break;
                }
            }
            if (index != - 1)
            {
                team.NFDBMembersBypass[index] = worker;
            }
            return team;
        }

        #endregion  // End of Private region

        #endregion // End of Methods
    }
}
