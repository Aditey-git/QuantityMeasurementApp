using System;
using QuantityAppModel;
using QuantityAppService;


namespace QuantityAppMain{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }
}