using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.ViewModels
{
    public class SpeechToTextViewModel
    {
        public IFormFile MyImage { set; get; }
    }
}
