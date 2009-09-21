using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G
{
    class ResourceManager
    {
        private static System.Resources.ResourceManager RM = null;
        public static System.Resources.ResourceManager Instance
        {
            get { return RM; }
        }

        public static void LoadResource(string name, System.Reflection.Assembly assembly)
        {
            if (RM == null)
            {
                RM = new System.Resources.ResourceManager(name, assembly);
            }
        }

    }
}
