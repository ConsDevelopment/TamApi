using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tam.Models.Interfaces
{
    public interface IDeletable
    {
        DateTime? DeleteTime { get; set;}
    }
}
