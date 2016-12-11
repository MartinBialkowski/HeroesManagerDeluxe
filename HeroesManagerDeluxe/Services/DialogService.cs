using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe
{
    public class DialogService
    {
        public List<string> PickedFiles { get; set; }
        public Action FilePicked { get; set; }
    }
}
