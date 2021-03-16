using Pinecorn;
using System.Collections.Generic;

namespace Game
{
    public class Spring : Component
    {
        private Dictionary<string, object> values;
        private float amount;
        public Spring(Dictionary<string,object> ogmoValues)
        {
            values = ogmoValues;
        }
        private enum Directions
        {
            up = -1,
            down = 1,
            right = -1,
            left = 1,
        };

        private Directions Direction;

        public override void Initialize()
        {
            values.TryGetValue("direction", out object dir);
            Direction = (Directions)dir;
        }

        public override void Update()
        {
            switch(Direction)
            {
                case Directions.left:
                        break;
            }
        }

        private void Bounce()
        {
            if(/*colliision*/ true)
            {
                var mover = entity.World.get("player").get<Mover>();
                
            }
        }


    }
}
