using Common;
using Common.Comparer;
using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsService
{
    public class Watcher
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        protected string directoryPath;
        Logger logger = LogManager.GetCurrentClassLogger();


        public Watcher(string directoryPath)
        {
            this.directoryPath = directoryPath;

            watcher = new FileSystemWatcher(directoryPath);
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;

            watcher.Filter = "*.xml";
            
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }

        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            logger.Info(string.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
            //RecordEntry(fileEvent, filePath);
        }

        // изменение файлов
       
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            AddPeople(sender, e);
            AddPayments(sender, e);
            logger.Info(string.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
            //RecordEntry(fileEvent, filePath);
        }

        // создание файлов
        
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            AddPeople(sender, e);
            AddPayments(sender, e);
            logger.Info(string.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
            //RecordEntry(fileEvent, filePath);
        }
        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            logger.Info(string.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
            //RecordEntry(fileEvent, filePath);
        }

        
        public void AddPeople(object sender, FileSystemEventArgs e)
        {
            if (IsTargetExtension(Path.GetExtension(e.FullPath)))
            {
                var people = new XMLDeserializator<List<Person>>();
                var table = people.Load(e.FullPath);
                var comparer = new PeopleCompare();
                
                if (table?.Any() == true)
                {
                    using (var context = new GeneratorContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] ON");
                            var clients = context.People.ToList();
                            var difClients = table.Except(clients, comparer);
                            context.AddRange(difClients);
                            context.SaveChanges();
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] OFF");
                            transaction.Commit();
                        }
                    }
                }
            }

        }
        
        public void AddPayments(object sender, FileSystemEventArgs e)
        {
            if (IsTargetExtension(Path.GetExtension(e.FullPath)))
            {
                var payments = new XMLDeserializator<List<Payment>>();
                var table = payments.Load(e.FullPath);
                var comparer = new PaymentsCompare();

                if (table?.Any() == true)
                {
                    using (var context = new GeneratorContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] ON");
                            var clientPayments = context.Payments.ToList();
                            var difPayments = table.Except(clientPayments, comparer);
                            context.AddRange(difPayments);
                            context.SaveChanges();
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] OFF");
                            transaction.Commit();
                        }
                    }
                }
            }

        }

        protected bool IsTargetExtension(string extension) => extension == watcher.Filter;

        //private void RecordEntry(string fileEvent, string filePath)
        //{

        //    string filepath = "C:\\templog.txt";
        //    lock (obj)
        //    {
        //        if (!File.Exists(filepath))
        //        {
        //            // Create a file to write to.   
        //            using (StreamWriter sw = File.CreateText(filepath))
        //            {
        //                sw.WriteLine(string.Format("{0} файл {1} был {2}",
        //                DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
        //            }
        //        }
        //        else
        //        {
        //            using (StreamWriter sw = File.AppendText(filepath))
        //            {
        //                sw.WriteLine(string.Format("{0} файл {1} был {2}",
        //                DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
        //            }
        //        }

        //    }
        //}
    }
}
