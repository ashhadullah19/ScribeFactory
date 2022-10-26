using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core
{
    public class VoiceModel
    {
        public string Gender { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public List<string> Engine { get; set; }
    }
}
