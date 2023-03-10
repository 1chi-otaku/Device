using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Device
{

    class Program
    {
        static void Main(string[] args)
        {
            Device device_1 = new Device("Dreamcast", "SEGA", 399.99);
            Device device_2 = new Device("Saturn", "SEGA", 299.99);
            Device device_3 = new Device("Mega Drive", "SEGA", 199.99);
            Device device_4 = new Device("PS 1", "Sony", 356.99);
            Device device_5 = new Device("Game Cube", "Nintendo", 499.99);
            Device device_6 = new Device("Atari 2600", "Atari", 99.99);
            Device device_7 = new Device("PS 2", "Sony", 489.99);
            Device device_8 = new Device("XBOX", "Microsoft", 409.99);

            List<Device> devices = new List<Device> { device_1, device_3, device_5, device_7 };
            List<Device> devices2 = new List<Device> { device_2, device_4, device_6, device_8 };

            Console.WriteLine("List 1:");
            foreach (var item in devices)
            {
                Console.WriteLine(item.Name + " – " + item.Manufacturer);
            }
            Console.WriteLine();
            Console.WriteLine("List 2:");
            foreach (var item in devices2)
            {
                Console.WriteLine(item.Name + " – " + item.Manufacturer);
            }

            Console.WriteLine();
            Console.WriteLine("Difference:");
            var device_diff = devices.Except(devices2, new ManufacturerSort());
            foreach (var device in device_diff)
            {
                Console.WriteLine(device.Name + " – " + device.Manufacturer);
            }
            Console.WriteLine();
            Console.WriteLine("Intersection:");
            var device_intersect = devices.Intersect(devices2, new ManufacturerSort());
            foreach (var device in device_intersect)
            {
                Console.WriteLine(device.Name + " – " + device.Manufacturer);
            }
            Console.WriteLine();
            Console.WriteLine("Fusion:");
            var device_fustion = devices.Concat(devices2);
            device_fustion = from u in device_fustion
                              orderby u.Manufacturer
                              select u;
            foreach (var device in device_fustion)
            {
                Console.WriteLine(device.Name + " – " + device.Manufacturer);
            }
            List<Device> fusion = device_fustion.ToList();

            #region 3/10/23
            Console.WriteLine();
            Console.ReadKey();

            int choice = -1;

            FileStream stream = null;
            XmlSerializer serializer = null;
            DataContractJsonSerializer jsonFormatter = null;
            while (choice != 0)
            {
                Console.Clear();
                Console.WriteLine("1 - Serialization XML");
                Console.WriteLine("2 - Deserialization XML");
                Console.WriteLine("3 - Serialization JSON");
                Console.WriteLine("4 - Deserialization JSON");
                Console.WriteLine("0 - Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        stream = new FileStream("../../DeviceXML.txt", FileMode.Create);
                        serializer = new XmlSerializer(typeof(List<Device>));
                        serializer.Serialize(stream, fusion);
                        stream.Close();
                        break;
                    case 2:
                        stream = new FileStream("../../DeviceXML.txt", FileMode.Open);
                        serializer = new XmlSerializer(typeof(List<Device>));
                        fusion = (List<Device>)serializer.Deserialize(stream);
                        string s = String.Empty;
                        foreach (var j in fusion)
                        {
                            Console.WriteLine(j.ToString());
                        }
                        Console.WriteLine(s);
                        stream.Close();
                        Console.ReadKey();
                        break;
                    case 3:
                        stream = new FileStream("../../DeviceJSON.json", FileMode.Create);
                        jsonFormatter = new DataContractJsonSerializer(typeof(List<Device>));
                        jsonFormatter.WriteObject(stream, fusion);
                        stream.Close();
                        Console.WriteLine("Сериализация успешно выполнена!");
                        break;
                    case 4:
                        stream = new FileStream("../../DeviceJSON.json", FileMode.Open);
                        jsonFormatter = new DataContractJsonSerializer(typeof(List<Device>));
                        fusion = (List<Device>)jsonFormatter.ReadObject(stream);
                        s = String.Empty;
                        foreach (var j in fusion)
                        {
                            Console.WriteLine(j.ToString());
                        }
                        stream.Close();
                        Console.ReadKey();
                        break;
                }
            }

            #endregion

        }
    }
} 
