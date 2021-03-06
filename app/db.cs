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
    }
}
