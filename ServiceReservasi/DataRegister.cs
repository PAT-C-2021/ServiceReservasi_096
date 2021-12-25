using System.Runtime.Serialization;

namespace ServiceReservasi
{
    [DataContract]
    public class DataRegister
    {
        [DataMember(Order = 1)]
        public int id { get; set;  }
        [DataMember(Order = 2)]
        public string username { get; set; }
        [DataMember(Order = 3)]
        public string password { get; set;  }
        [DataMember(Order = 4)]
        public string kategori { get; set;  }
    }
}