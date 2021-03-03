using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    class db : SpeedrunEntities
    {
        // singleton (yes I'm using inheritance for code copying, have a nice day :)
        private db() { }
        private static db _db = null;
        public static db GetAccessPoint()
        {
            if (_db == null)
                _db = new db();

            return _db;
        }

        private object GetDBSet(string name)
        {
            MethodInfo GetSetMethod = _db.GetType().GetMethod("get_" + name);
            return GetSetMethod.Invoke(_db, null);
        }

        public void Add(object obj)
        {
            Type type = obj.GetType();

            object dbSet = _db.GetDBSet(type.Name);
            dbSet.GetType().GetMethod("Add").Invoke(dbSet, new object[] { obj });
            _db.SaveChanges();
        }

        public void Remove(object obj)
        {
            object DBSet = GetDBSet(obj.GetType().Name);

            MethodInfo removeMethod = DBSet.GetType().GetMethod("Remove");

            removeMethod.Invoke(DBSet, new object[] { obj });

            _db.SaveChanges();
        }
    }
}
