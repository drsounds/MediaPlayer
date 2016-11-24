using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class Model
    {
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public string Url { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
