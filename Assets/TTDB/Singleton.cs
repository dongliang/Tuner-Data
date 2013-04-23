/*
   Tuner Data - Used to read the static data  in game development.
   e-mail : dongliang17@126.com  
*/
using System.Diagnostics;
namespace TD
{
    public class Singleton<T> where T : new()
    {
        protected Singleton() { Debug.Assert(null == instance); }
        protected static T instance = new T();
        public static T Instance
        {
            get { return instance; }
        }
    }
}
