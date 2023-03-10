using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Device
{
    [Serializable]
    [DataContract]
    public class Device
    {

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Manufacturer { get; set; }
        [DataMember]
        public double Price { get; set; }

        public Device()
        {
            Name = "N/A";
            Manufacturer = "N/A";
            Price = 0.0;
        }
        public Device(string name, string manufacturer, double price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }

        public override string ToString()
        {
            return $"Name - {Name}\nManufacturer - {Manufacturer}\nPrice - {Price}";
        }
    }
    public class ManufacturerSort: IEqualityComparer<Device>
    {
        public bool Equals(Device device_1, Device device_2)
        {
            if (device_1.Manufacturer == device_2.Manufacturer) return true;
            return false;
        }
        public int GetHashCode(Device obj)
        {
            return obj.Manufacturer.GetHashCode();
        }
    }
}
