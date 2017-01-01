using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TeamNectarineScheduleManager.Users;

namespace TeamNectarineScheduleManager.DataBaseLibrary
{
    public static class DataBase
    {
        // Fields
        #region
        private static string dataDir = "Data";
        private static string custDir = "Data\\Custom";
        private static Exception excFileNotFound = new Exception("No file corresponds to the given input.");
        private static Exception excIOError = new Exception("Input/Output error.");
        #endregion

        // Methods
        #region
        /// <summary>
        /// Saves a worker object on the hard drive, so it can be loaded later.
        /// </summary>
        public static void WriteToDisk(Worker worker)
        {
            try
            {
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                // Create coresponding file name.

                string fileName = string.Format("{0}\\W{1}.bin", dataDir, worker.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disk and close the stream.
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
        public static void WriteToDisk(Administrator administrator)
        {
            try
            {
                if (!Directory.Exists(custDir))
                {
                    Directory.CreateDirectory(custDir);
                }
                // Create coresponding file name.

                string fileName = string.Format("{0}\\A{1}", custDir, administrator.Username);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disk and close the stream.
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
        /// Loads a worker object from the disk.
        /// </summary>
        /// <param name="userName">The name of the worker.</param>
        /// <returns></returns>
        public static Worker ReadFromDiskWorker(string userName)
        {
            try
            {
                // Create coresponding file name.
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
        /// Loads an admin object from the disk.
        /// </summary>
        /// <param name="userName">The name of the admin.</param>
        /// <returns></returns>
        public static Administrator ReadFromDiskAdmin(string userName)
        {
            try
            {
                // Create coresponding file name.
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
        public static void WriteToDiskGeneric(object obj, string fileName)
        {
            try
            {
                if (!Directory.Exists(custDir))
                {
                    Directory.CreateDirectory(custDir);
                }
                // Create coresponding file name.

                fileName = string.Format("{0}\\{1}", custDir, fileName);
                // Assign filestream to a new file with the file name.
                Stream stream = File.Create(fileName);
                // Init formatter, write  to disk and close the stream.
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
        public static object ReadFromDiskGeneric(string fileName)
        {
            try
            {
                // Create coresponding file name.
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
        /// Returns a list of all of the workers, that are saved on the disk.
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
        /// Returns a list of all of the admins, that are saved on the disk.
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
        /// Returns a list of the user names from all of the users, that are saved on the disk.
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
        /// Checks if the given login data matches any user, saved on the disk.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns></returns>
        public static bool IsValidLoginData(string userName, string password)
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Administrator> allAdmins = GetAllAdmins();

            // Check wokers
            foreach (var worker in allWorkers)
            {
                if (worker.Username == userName && worker.Password == password)
                {
                    return true;
                }
            }

            // check admins
            foreach (var admin in allAdmins)
            {
                if (admin.Username == userName && admin.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns UserType.Admin if an admin with the given user name exists on the disk, and UserType.Worker if a worker with the given user name exists on the disk. Returns UserType.Unknown if there isn't a user with that name on the disk.
        /// </summary>
        /// <param name="userName"></param>
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
        #endregion
    }
}
