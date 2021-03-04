using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace appTesting
{
    class DB : SpeedrunEntities
    {
        private object GetSet(String setName)
        {
            MethodInfo getSetMethod = GetType().GetMethod("get_" + setName);
            return getSetMethod.Invoke(this, null);
        }

        public void Add(object instance)
        {
            object set = GetSet(instance.GetType().Name);
            MethodInfo addMethod = set.GetType().GetMethod("Add");
            addMethod.Invoke(set, new object[] { instance });
            SaveChanges();
        }
    }
}
