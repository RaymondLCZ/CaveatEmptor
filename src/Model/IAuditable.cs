using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    /// <summary> A marker interface for auditable persistent domain classes. </summary>
    public interface IAuditable
    {
        long Id { get; }
    }
}
