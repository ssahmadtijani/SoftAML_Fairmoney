using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftAML_UpperLinkAPI.Models
{
    public class PepListDTO
    {
        public string UniqueIdentity { get; set; }
        public string FullName { get; set; }
        public string PepClassification { get; set; }

        public string PreviousPosition { get; set; }
        public string PresentPosition { get; set; }
    }
}