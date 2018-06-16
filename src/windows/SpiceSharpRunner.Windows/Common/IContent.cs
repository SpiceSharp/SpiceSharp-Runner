﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiceSharpRunner.Windows.Common
{
    public interface IContent
    {
        string Title { get; }

        bool CanClose { get; }

        bool IsResizable { get; }
    }
}
