using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Persistence.Settings.Configurations {
    public class PhotoSettings {
        [Required, MinLength(2)]
        public int[] ChannelPhotoWidths { get; set; } = null!;
        
        [Required, Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PhotoQuallity { get; set; }
    }
}
