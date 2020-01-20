using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ObjectPersister;

namespace ObjectPersisterConsole
{
    class Program
    {
        private static ObjectPersister.ObjectPersister _objectPersister = new ObjectPersister.ObjectPersister();
        private static ObjectPersisterDbContext db;

        static Program()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ObjectPersisterDbContext>();
            optionsBuilder.UseSqlite("Data Source=database.db");
            db = new ObjectPersisterDbContext(optionsBuilder.Options);
            db.Database.EnsureCreated();

            _objectPersister.ObjectDefinitions = db.ObjectDefinitions
                .Include(od => od.Properties)
                .Include(od => od.Objects).ThenInclude(o => o.Properties).ThenInclude(p => p.Definition)
                .ToDictionary(od => od.Name);
            _objectPersister.Objects = _objectPersister.ObjectDefinitions.Values.SelectMany(od => od.Objects).ToList();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to object persister");

            while (true)
            {
                Console.WriteLine("Choose Action:");
                Console.WriteLine("1. Define new object");
                Console.WriteLine("2. Create new object instance");
                Console.WriteLine("3. Dump all objects");
                Console.WriteLine("4. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        DefineObject();
                        break;
                    case "2":
                        CreateObject();
                        break;
                    case "3":
                        _objectPersister.DumpObjects();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye");
                        db.SaveChanges();
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        private static void DefineObject()
        {
            Console.WriteLine("Define object name");
            var name = Console.ReadLine();
            Console.WriteLine("Define property count");
            int propertyCount;
            while (!int.TryParse(Console.ReadLine(), out propertyCount))
            {
                Console.WriteLine("Invalid number");
            }

            var properties = new Dictionary<string, string>();
            var constraints = new Dictionary<string, List<string>>();
            while (properties.Count != propertyCount)
            {
                Console.WriteLine("Define property name");
                var pName = Console.ReadLine();
                Console.WriteLine("Define property type (integer, string or enum)");
                var pType = Console.ReadLine();
                properties[pName] = pType;
                constraints[pName] = new List<string>();
                //TODO: Move to ObjectPersister
                if (pType == "enum" || pType == "?enum")
                {
                    Console.WriteLine("Define number of enum values");
                    int n = int.Parse(Console.ReadLine());
                    Console.WriteLine("Define enum values");
                    for (int i = 0; i < n; i++)
                    {
                        constraints[pName].Add(Console.ReadLine());
                    }
                }
            }

            try
            {
                var objDef = _objectPersister.DefineObject(name, properties, constraints);
                db.Add(objDef);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private static void CreateObject()
        {
            if (_objectPersister.ObjectDefinitions.Count == 0)
            {
                Console.WriteLine("System has no defined objects");
                return;
            }

            string name;
            do
            {
                Console.WriteLine("Which object do you want to create?");
                foreach (var objectName in _objectPersister.ObjectDefinitions.Keys)
                {
                    Console.Write(objectName + " ");
                }

                Console.WriteLine();
                name = Console.ReadLine();
            } while (!_objectPersister.ObjectDefinitions.ContainsKey(name));

            var properties = new Dictionary<string, string>();
            foreach (var propertyDefinition in _objectPersister.ObjectDefinitions[name].Properties)
            {
                Console.WriteLine(
                    "Input value for '" + propertyDefinition.Name + "' of type " + propertyDefinition.Type);
                var value = Console.ReadLine();
                if (value == "")
                {
                    value = null;
                }

                properties[propertyDefinition.Name] = value;
            }

            try
            {
                var obj = _objectPersister.CreateObject(name, properties);
                db.Add(obj);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}