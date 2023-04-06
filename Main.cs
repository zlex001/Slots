using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    public static Slot.Manager slot = new Slot.Manager();
    static void Main(string[] args)
    {
        slot.Start();
    }
}

