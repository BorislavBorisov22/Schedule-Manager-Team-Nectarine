using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TeamNectarineScheduleManager.Users;

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
        /// <summary>
        /// Saves a worker object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void WriteToDisc(Worker worker)
        {
            if (object.ReferenceEquals(worker, null))
            {

            }

            try
            {

                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create corresponding file name.

                string fileName = string.Format("{0}\\W{1}.bin", dataDir, worker.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, worker);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Saves an admin object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void WriteToDisc(Administrator administrator)
        {
            try
            {
                if (!Directory.Exists(custDir))
                {
                    Directory.CreateDirectory(custDir);
                }

                // Create corresponding file name.
                string fileName = string.Format("{0}\\A{1}.bin", dataDir, administrator.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disc and close the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, fileName);
                stream.Close();
            }
            catch
            {
                throw excIOError;
            }
        }

        /// <summary>
        /// Loads a worker object from the disc.
        /// </summary>
        /// <param name="userName">The name of the worker.</param>
        /// <returns></returns>
        public static Worker ReadFromDiscWorker(string userName)
        {
            try
            {
                // Create corresponding file name.
                string fileNameWorker = string.Format("{0}\\W{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameWorker))
                {
                    Stream stream = File.OpenRead(fileNameWorker);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Worker result = (Worker)formatter.Deserialize(stream);
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
                string fileNameWorker = string.Format("{0}\\A{1}.bin", dataDir, userName);
                // Check if the file exists;
                if (File.Exists(fileNameWorker))
                {
                    Stream stream = File.OpenRead(fileNameWorker);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Administrator result = (Administrator)formatter.Deserialize(stream);
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

        /// <summary>
        /// Saves any object to the hard drive. If a file with the given name already exists it will be replaced.
        /// </summary>
        /// <param name="obj">The object to be saved.</param>
        /// <param name="fileName">The name of the file.</param>
        public static void WriteToDiscGeneric(object obj, string fileName)
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
        public static object ReadFromDiscGeneric(string fileName)
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

        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        /// <param name="userName"></param>
        public static void DeleteUserFromDisc(string userName)
        {
            try
            {
                if (UserExists(userName))
                {
                    if (GetUserType(userName) == UserType.Admin)
                    {
                        File.Delete(string.Format("{0}\\A{1}bin", dataDir, userName));
                    }
                    else if (GetUserType(userName) == UserType.Worker)
                    {
                        File.Delete(string.Format("{0}\\A{1}bin", dataDir, userName));
                    }
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
        public static void DeleteUserFromDisc(Administrator admin)
        {
            DeleteUserFromDisc(admin.Username);
        }

        /// <summary>
        /// Deletes the file, storing this user.
        /// </summary>
        public static void DeleteUserFromDisc(Worker worker)
        {
            DeleteUserFromDisc(worker.Username);
        }

        /// <summary>
        /// Checks if a user with the given name exists. Returns true if it does, and false if it doesn't.
        /// </summary>
        /// <param name="userName">The name of the user to check.</param>
        /// <returns></returns>
        public static bool UserExists(string userName)
        {
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName))
                || File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
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
        public static List<Worker> GetAllWorkers()
        {
            string[] files = Directory.GetFiles(dataDir);
            List<Worker> workers = new List<Worker>();
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    Stream stream = File.OpenRead(files[i]);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Worker result = (Worker)formatter.Deserialize(stream);
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
        /// Returns a list of the user names from all of the users, that are saved on the disc.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllUserNames()
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Administrator> allAdmins = GetAllAdmins();
            List<string> userNames = new List<string>();
            for (int i = 0; i < allWorkers.Count; i++)
            {
                userNames.Add(allWorkers[i].Username);
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
            // check admins
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

            // Check wokers
            else if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
            {
                if (ReadFromDiscWorker(userName).Password == password)
                {
                    return true;
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
                && (!File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName))))
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
            foreach (var fileName in Directory.GetFiles(dataDir))
            {
                try
                {

                    if (fileName[0] == 'A')
                    {
                        try
                        {
                            string userName = fileName.Substring(fileName.LastIndexOf('\\') + 1, fileName.Length - fileName.LastIndexOf('\\') - 6);
                            Administrator admin = ReadFromDiscAdmin(userName);
                            if (admin.Username != userName)
                            {
                                File.Delete(string.Format("{0}\\{1}", dataDir, fileName));
                            }
                        }
                        catch
                        {
                            File.Delete(string.Format("{0}\\{1}", dataDir, fileName));
                        }
                    }
                    else if (fileName[0] == 'W')
                    {
                        try
                        {
                            string userName = fileName.Substring(fileName.LastIndexOf('\\') + 1, fileName.Length - fileName.LastIndexOf('\\') - 6);
                            Worker worker = ReadFromDiscWorker(userName);
                            if (worker.Username != userName)
                            {
                                File.Delete(string.Format("{0}\\{1}", dataDir, fileName));
                            }
                        }
                        catch
                        {
                            File.Delete(string.Format("{0}\\{1}", dataDir, fileName));
                        }
                    }
                    else
                    {
                        File.Delete(string.Format("{0}\\{1}", dataDir, fileName));
                    }
                }
                catch { }
            }
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
                    ReadFromDiscWorker(userName);
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
        public static bool IsUserFileWholesome(Administrator admin)
        {
            // check admins
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, admin.Username)))
            {
                try
                {
                    ReadFromDiscAdmin(admin.Username);
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
        public static bool IsUserFileWholesome(Worker worker)
        {
            // check workers
            if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, worker.Username)))
            {
                try
                {
                    ReadFromDiscWorker(worker.Username);
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
        /// Returns UserType.Admin if an admin with the given user name exists on the disc, and UserType.Worker if a worker with the given user name exists on the disc. Returns UserType.Unknown if there isn't a user with that name on the disc.
        /// </summary>
        /// <returns></returns>
        public static UserType GetUserType(string userName)
        {
            if (File.Exists(string.Format("{0}\\A{1}.bin", dataDir, userName)))
            {
                return UserType.Admin;
            }
            if (File.Exists(string.Format("{0}\\W{1}.bin", dataDir, userName)))
            {
                return UserType.Worker;
            }
            return UserType.Unknown;
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

        #endregion
    }
}
