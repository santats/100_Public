using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopOnline.ProductMicroservice.Entities
{
    public class Product
    {
        [XmlAttribute]
        public String Tags { get; set; }

        [XmlAttribute]
        public String DisplayName { get; set; }

        [XmlAttribute]
        public float Price { get; set; }

        [XmlAttribute]
        public Guid Id { get; set; }
    }
}
