using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.Models
{
    public class Cache
    {
        private static IDictionary<string, Object> data = null;
        private static IDictionary<string, Object> state = null;

        public static IDictionary<string, Object> getInstance()
        {
            if (data == null)
            {
                data = new Dictionary<string, object>();
            }
            return data;
        }

        public static IDictionary<string, Object> getStateInstance()
        {
            if (state == null)
            {
                state = new Dictionary<string, object>();
            }
            return state;
        }


    }
}
