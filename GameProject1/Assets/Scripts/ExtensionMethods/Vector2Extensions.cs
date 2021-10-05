using Bolt;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Vector2Extensions
    {
        public static Vector2 GetDirections4(this Vector2 self)
        {
            if (Mathf.Abs(self.x) > Mathf.Abs(self.y))
            {
                return new Vector2(Mathf.Sign(self.x), 0);
            }
            
            if(Mathf.Abs(self.y) > Mathf.Abs(self.x))
            {
                return new Vector2(0,Mathf.Sign(self.y));
            }

            return Vector2.zero;
        }
    }
}