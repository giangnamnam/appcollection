﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Component
{
    [System.Runtime.Serialization.DataContract]
    public enum IrisMode
    {
        [System.Runtime.Serialization.EnumMember]
        Auto,

        [System.Runtime.Serialization.EnumMember]
        Manual,
    }
}
