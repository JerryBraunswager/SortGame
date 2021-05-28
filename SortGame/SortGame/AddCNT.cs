using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortGame
{
    class AddCNT
    {
        int balls_count, caps_count;
        int g_count = 0;
        public int caps { get { return caps_count; } }
        public int balls { get { return balls_count; } }
        public void new_count(int _balls_count, int _caps_count)
        {
            //g_count = 0;
            balls_count = _balls_count;
            caps_count = _caps_count;
        }
        public void g_continue()
        {
            g_count += 1;
            balls_count += (balls_count <= g_count) ? 1 : 0;
            caps_count += (caps_count <= g_count) ? 1 : 0;
            if (balls_count <= g_count | caps_count <= g_count)
                g_count = 0;
        }
    }
}
