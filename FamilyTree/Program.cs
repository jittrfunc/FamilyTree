using System;
using FamilyTree.ConsoleUtilities;

namespace FamilyTree
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                FamilyTreeExecutor executor = new FamilyTreeExecutor(args[0]);
                executor.Run();
            }
        }
    }
}
