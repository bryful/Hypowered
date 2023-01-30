using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hpd
{
    public class ListItem
    {
        public string Text { get; set; } = "";
        public string Path { get; set; } = "";
        public object? Obj { get; set; } = null;
        public ListItem()
        {

        }
        public ListItem(string n)
        {
            Text = n;
        }
        public ListItem(string n, object? o)
        {
            Text = n;
            Obj = o;
        }
        public ListItem(string n, string p)
        {
            Text = n;
            Path = p;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}