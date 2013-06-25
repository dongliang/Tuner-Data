/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System.Diagnostics;
namespace Tuner
{
    public class Singleton<T> where T : new()
    {
        protected Singleton() {
            //Debug.Assert(null == instance);
        }
        protected static T instance = new T();
        public static T Instance
        {
            get { return instance; }
        }
    }
}
