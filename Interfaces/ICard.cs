using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Interfaces
{
    public interface ICard
    {
        public string Text { get; }

        public int PlayEffect();
    }
}
