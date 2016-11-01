using HeroesManagerDeluxe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe
{
    public class DisplayWorkspaceMessage
    {
        public WorkspaceViewModel workspace { get; private set; }

        public DisplayWorkspaceMessage(WorkspaceViewModel workspace)
        {
            this.workspace = workspace;
        }
    }
}
