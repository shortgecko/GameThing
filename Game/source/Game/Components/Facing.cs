using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;

namespace Game
{
    class Facing : Component
    {
        private int Get()
        {
            Mover mover = Entity.Get<Mover>();
            int sign = Math.Sign(mover.Move.X);
            if (sign != 0)
                return sign;
            return 0;
        }

        public static implicit operator int(Facing facing) => facing.Get();
    }
}
